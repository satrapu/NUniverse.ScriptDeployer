namespace NUniverse.ScriptDeployer.Core
{
    /// <summary>
    /// Deployes database packages.
    /// </summary>
    public class DeployerTool
    {
        /// <summary>
        /// Gets or sets the <seealso cref="IDatabasePackageScanner"/> implementation 
        /// to be used when scanning for database packages available for deploying.
        /// </summary>
        public IDatabasePackageScanner Scanner
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets tkhe <seealso cref="IDatabasePackageDeployer"/> implementation to be used when deploying 
        /// database packages.
        /// </summary>
        public IDatabasePackageDeployer Deployer
        {
            get;
            set;
        }

        /// <summary>
        /// Creates a new instance of <seealso cref="DeployerTool"/> class.
        /// </summary>
        /// <param name="scanner">The <seealso cref="IDatabasePackageScanner"/> implementation 
        /// to be used when scanning for database packages available for deploying.</param>
        /// <param name="deployer">The <seealso cref="IDatabasePackageDeployer"/> implementation to be used when 
        /// deploying database packages.</param>
        public DeployerTool(IDatabasePackageScanner scanner, IDatabasePackageDeployer deployer)
        {
            Scanner = scanner;
            Deployer = deployer;
        }

        /// <summary>
        /// Rollouts the scripts belonging to the database packages.
        /// </summary>
        public void Rollout()
        {
            Deployer.Rollout(Scanner.GetPackages());
        }

        /// <summary>
        /// Rollbacks the scripts belonging to the database packages.
        /// </summary>
        public void Rollback()
        {
            Deployer.Rollback(Scanner.GetPackages());
        }
    }
}