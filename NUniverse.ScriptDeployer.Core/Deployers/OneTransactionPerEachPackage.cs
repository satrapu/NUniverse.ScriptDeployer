using System.Collections.Generic;
using System.Data;

namespace NUniverse.ScriptDeployer.Core.Deployers
{
    /// <summary>
    /// <seealso cref="IDatabasePackageDeployer"/> implementation which uses one local ADO.NET transaction for each database package to be deployed.
    /// </summary>
    public class OneTransactionPerEachPackage : IDatabasePackageDeployer
    {
        public void Rollout(IEnumerable<DatabasePackage> packages)
        {
            List<DatabasePackage> deployableDatabasePackages = new List<DatabasePackage>(packages);

            if (deployableDatabasePackages.Count == 0)
            {
                return;
            }

            foreach (DatabasePackage databasePackage in deployableDatabasePackages)
            {
                IDbConnection dbConnection = null;
                IDbTransaction dbTransaction = null;

                try
                {
                    dbConnection = databasePackage.Provider.CreateConnection();
                    dbConnection.ConnectionString = databasePackage.ConnectionString;
                    dbConnection.Open();
                    dbTransaction = dbConnection.BeginTransaction();

                    foreach (DatabaseScript script in databasePackage.RolloutScripts)
                    {
                        using (IDbCommand dbCommand = dbConnection.CreateCommand())
                        {
                            dbCommand.CommandType = CommandType.Text;
                            dbCommand.Transaction = dbTransaction;
                            dbCommand.CommandText = script.Content;
                            dbCommand.ExecuteNonQuery();
                        }
                    }

                    dbTransaction.Commit();
                }
                catch
                {
                    if (dbTransaction != null)
                    {
                        dbTransaction.Rollback();
                    }

                    throw;
                }
                finally
                {
                    if (dbTransaction != null)
                    {
                        dbTransaction.Dispose();
                    }

                    if (dbConnection != null)
                    {
                        dbConnection.Close();
                        dbConnection.Dispose();
                    }
                }
            }
        }

        public void Rollback(IEnumerable<DatabasePackage> packages)
        {
            List<DatabasePackage> deployableDatabasePackages = new List<DatabasePackage>(packages);

            if (deployableDatabasePackages.Count == 0)
            {
                return;
            }

            foreach (DatabasePackage databasePackage in deployableDatabasePackages)
            {
                IDbConnection dbConnection = null;
                IDbTransaction dbTransaction = null;

                try
                {
                    dbConnection = databasePackage.Provider.CreateConnection();
                    dbConnection.ConnectionString = databasePackage.ConnectionString;
                    dbConnection.Open();
                    dbTransaction = dbConnection.BeginTransaction();

                    foreach (DatabaseScript script in databasePackage.RollbackScripts)
                    {
                        using (IDbCommand dbCommand = dbConnection.CreateCommand())
                        {
                            dbCommand.CommandType = CommandType.Text;
                            dbCommand.Transaction = dbTransaction;
                            dbCommand.CommandText = script.Content;
                            dbCommand.ExecuteNonQuery();
                        }
                    }

                    dbTransaction.Commit();
                }
                catch
                {
                    if (dbTransaction != null)
                    {
                        dbTransaction.Rollback();
                    }

                    throw;
                }
                finally
                {
                    if (dbTransaction != null)
                    {
                        dbTransaction.Dispose();
                    }

                    if (dbConnection != null)
                    {
                        dbConnection.Close();
                        dbConnection.Dispose();
                    }
                }
            }
        }
    }
}