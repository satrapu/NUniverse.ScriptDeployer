using System.Collections.Generic;

namespace SqlScriptsDeployer.Core
{
    /// <summary>
    /// Scans for database packages available for deployment.
    /// </summary>
    public interface IDatabasePackageScanner
    {
        /// <summary>
        /// Scans for database packages available for deployment.
        /// </summary>
        /// <returns>All database packages available for deployment.</returns>
        IEnumerable<DatabasePackage> GetPackages();
    }
}