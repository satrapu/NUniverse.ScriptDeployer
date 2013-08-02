using System.Data.Common;

namespace SqlScriptsDeployer.Core
{
    /// <summary>
    /// Represents a relational database where one or more SQL scripts will be deployed to.
    /// </summary>
    public class Database
    {
        /// <summary>
        /// Gets or sets the underlying ADO.NET provider used for deploying SQL scripts.
        /// </summary>
        public DbProviderFactory Provider
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the connection string used for accessing the relational database.
        /// </summary>
        public string ConnectionString
        {
            get;
            set;
        }
    }
}