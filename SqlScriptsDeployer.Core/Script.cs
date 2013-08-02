namespace SqlScriptsDeployer.Core
{
    /// <summary>
    /// Represents a SQL script to be deployed.
    /// </summary>
    public class Script
    {
        /// <summary>
        /// Gets or sets the SQL statements composing the script.
        /// </summary>
        public string Content
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the script location (file path, URL, etc.).
        /// </summary>
        public string Location
        {
            get;
            set;
        }
    }
}