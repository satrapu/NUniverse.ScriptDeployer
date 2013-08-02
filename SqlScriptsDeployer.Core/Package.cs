using System.Collections.Generic;

namespace SqlScriptsDeployer.Core
{
    /// <summary>
    /// Represents a sum of SQL scripts to be deployed to one or more relational databases.
    /// </summary>
    public class Package
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
        /// Gets or sets the list of relational databases where the SQL scripts will be deployed to.
        /// </summary>
        public IList<Database> Targets
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the SQL scripts to be deployed.
        /// </summary>
        public IList<Script> Scripts
        {
            get;
            set;
        }
    }
}