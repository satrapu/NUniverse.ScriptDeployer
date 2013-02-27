using System.Collections.Generic;
using System.Data;
using System.Transactions;

namespace NUniverse.ScriptDeployer.Core.Deployers
{
    /// <summary>
    /// <seealso cref="IDatabasePackageDeployer"/> implementation which uses one distributed ADO.NET transaction for all database packages to be deployed.
    /// </summary>
    public class OneTransactionPerAllPackages : IDatabasePackageDeployer
    {
        public void Rollout(IEnumerable<DatabasePackage> packages)
        {
            List<DatabasePackage> deployableDatabasePackages = new List<DatabasePackage>(packages);

            if (deployableDatabasePackages.Count == 0)
            {
                return;
            }

            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = System.Transactions.IsolationLevel.Serializable;

            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                foreach (DatabasePackage databasePackage in deployableDatabasePackages)
                {
                    using (IDbConnection dbConnection = databasePackage.Provider.CreateConnection())
                    {
                        dbConnection.ConnectionString = databasePackage.ConnectionString;
                        dbConnection.Open();

                        foreach (DatabaseScript script in databasePackage.RolloutScripts)
                        {
                            using (IDbCommand dbCommand = dbConnection.CreateCommand())
                            {
                                dbCommand.CommandType = CommandType.Text;
                                dbCommand.CommandText = script.Content;
                                dbCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }

                transactionScope.Complete();
            }
        }

        public void Rollback(IEnumerable<DatabasePackage> packages)
        {
            List<DatabasePackage> deployableDatabasePackages = new List<DatabasePackage>(packages);

            if (deployableDatabasePackages.Count == 0)
            {
                return;
            }

            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = System.Transactions.IsolationLevel.Serializable;

            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                foreach (DatabasePackage databasePackage in deployableDatabasePackages)
                {
                    using (IDbConnection dbConnection = databasePackage.Provider.CreateConnection())
                    {
                        dbConnection.ConnectionString = databasePackage.ConnectionString;
                        dbConnection.Open();

                        foreach (DatabaseScript script in databasePackage.RollbackScripts)
                        {
                            using (IDbCommand dbCommand = dbConnection.CreateCommand())
                            {
                                dbCommand.CommandType = CommandType.Text;
                                dbCommand.CommandText = script.Content;
                                dbCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }

                transactionScope.Complete();
            }
        }
    }
}