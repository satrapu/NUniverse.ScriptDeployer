namespace NUniverse.ScriptDeployer.Core
{
    /// <summary>
    /// Encapsulates one SQL script to be deployed against a database.
    /// </summary>
    public class DatabaseScript
    {
        /// <summary>
        /// Gets or sets the script name.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the SQL statements which compose this script.
        /// </summary>
        public string Content
        {
            get;
            set;
        }
    }
}