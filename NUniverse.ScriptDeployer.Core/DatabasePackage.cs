using System.Collections.Generic;
using System.Data.Common;

namespace NUniverse.ScriptDeployer.Core
{
    /// <summary>
    /// Represents a sum of SQL scripts to be deployed against a database.
    /// </summary>
    public class DatabasePackage
    {
        /// <summary>
        /// Gets or sets the name of this package.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the rollout SQL scripts to be deployed against the database.
        /// </summary>
        public IEnumerable<DatabaseScript> RolloutScripts
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the rollback SQL scripts to be deployed against the database.
        /// </summary>
        public IEnumerable<DatabaseScript> RollbackScripts
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the ADO.NET provider used for communicating with the database.
        /// </summary>
        public DbProviderFactory Provider
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the connection string to the database.
        /// </summary>
        public string ConnectionString
        {
            get;
            set;
        }
    }
}