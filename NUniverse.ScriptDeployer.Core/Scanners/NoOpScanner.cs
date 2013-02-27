using System.Collections.Generic;

namespace NUniverse.ScriptDeployer.Core.Scanners
{
    /// <summary>
    /// <seealso cref="IDatabasePackageScanner"/> implementation which returns the database packages received during construction.
    /// </summary>
    public class NoOpScanner : IDatabasePackageScanner
    {
        private List<DatabasePackage> packages;

        /// <summary>
        /// Creates a new instance of <seealso cref="NoOpScanner"/> class.
        /// </summary>
        /// <param name="packages"></param>
        public NoOpScanner(IEnumerable<DatabasePackage> packages)
        {
            this.packages = new List<DatabasePackage>(packages);
        }

        public IEnumerable<DatabasePackage> GetPackages()
        {
            return packages;
        }
    }
}