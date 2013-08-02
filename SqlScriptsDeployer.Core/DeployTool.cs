using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;

namespace SqlScriptsDeployer.Core
{
    /// <summary>
    /// Tool used for deploying SQL scripts to relational databases.
    /// </summary>
    public class DeployTool
    {
        /// <summary>
        /// Deploys the given <seealso cref="Package"/> instances.
        /// </summary>
        /// <param name="packages">The <seealso cref="Package"/> instances to deploy.</param>
        /// <param name="useDistributedTransaction"></param>
        public void Deploy(IEnumerable<Package> packages, bool useDistributedTransaction = true)
        {
            if (packages == null)
            {
                throw new ArgumentNullException("packages");
            }

            IList<Package> packageList = packages as IList<Package> ?? packages.ToList();

            if (useDistributedTransaction)
            {
                DeployUsingDistributedTransaction(packageList, System.Transactions.IsolationLevel.Serializable);
            }
            else
            {
                DeployUsingLocalTransaction(packageList, System.Data.IsolationLevel.Serializable);
            }
        }

        /// <summary>
        /// Deploys the given <paramref name="packages"/> using a distributed transaction.
        /// </summary>
        /// <param name="packages">The <seealso cref="Package"/> instances to deploy.</param>
        /// <param name="isolationLevel">The transaction isolation level to be used.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        protected virtual void DeployUsingDistributedTransaction(IEnumerable<Package> packages, System.Transactions.IsolationLevel isolationLevel)
        {
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required,
                    new TransactionOptions {IsolationLevel = isolationLevel}))
            {
                foreach (Package package in packages)
                {
                    foreach (Database database in package.Targets)
                    {
                        using (IDbConnection connection = database.Provider.CreateConnection())
                        {
                            if (connection == null)
                            {
                                throw new InvalidOperationException(string.Format("Unable to create connection using provider: {0}",
                                        database.Provider.GetType().AssemblyQualifiedName));
                            }

                            connection.ConnectionString = database.ConnectionString;
                            connection.Open();

                            foreach (Script script in package.Scripts)
                            {
                                using (IDbCommand command = connection.CreateCommand())
                                {
                                    command.CommandText = script.Content;
                                    command.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }

                transactionScope.Complete();
            }
        }

        /// <summary>
        /// Deploys the given <paramref name="packages"/> using local transactions (one per opened connection).
        /// </summary>
        /// <param name="packages"></param>
        /// <param name="isolationLevel">The transaction isolation level to be used.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        protected virtual void DeployUsingLocalTransaction(IEnumerable<Package> packages, System.Data.IsolationLevel isolationLevel)
        {
            foreach (Package package in packages)
            {
                foreach (Database database in package.Targets)
                {
                    IDbConnection connection = null;
                    IDbTransaction transaction = null;

                    try
                    {
                        connection = database.Provider.CreateConnection();

                        if (connection == null)
                        {
                            throw new InvalidOperationException(string.Format("Unable to create connection using provider: {0}",
                                    database.Provider.GetType().AssemblyQualifiedName));
                        }

                        connection.ConnectionString = database.ConnectionString;
                        connection.Open();
                        transaction = connection.BeginTransaction(isolationLevel);

                        foreach (Script script in package.Scripts)
                        {
                            using (IDbCommand command = connection.CreateCommand())
                            {
                                command.CommandText = script.Content;
                                command.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        if (transaction != null)
                        {
                            transaction.Rollback();
                        }

                        throw;
                    }
                    finally
                    {
                        if (transaction != null)
                        {
                            transaction.Dispose();
                        }

                        if (connection != null)
                        {
                            connection.Close();
                        }
                    }
                }
            }
        }
    }
}