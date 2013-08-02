using System.Collections.Generic;

namespace SqlScriptsDeployer.Core
{
    /// <summary>
    /// Deploys database packages.
    /// </summary>
    public interface IDatabasePackageDeployer
    {
        /// <summary>
        /// Rollouts the given database packages.
        /// </summary>
        /// <param name="packages">The packages to be deployed.</param>
        void Rollout(IEnumerable<DatabasePackage> packages);

        /// <summary>
        /// Rollbacks the given database packages.
        /// </summary>
        /// <param name="packages">The packages to be deployed.</param>
        void Rollback(IEnumerable<DatabasePackage> packages);
    }
}