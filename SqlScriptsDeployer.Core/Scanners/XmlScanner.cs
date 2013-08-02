using System.Collections.Generic;
using System.Linq;

namespace SqlScriptsDeployer.Core.Scanners
{
    /// <summary>
    /// <seealso cref="IDatabasePackageScanner"/> implementation which fetches the list of database packages available for deployment
    /// by reading an XML configuration file.
    /// </summary>
    public class XmlScanner : IDatabasePackageScanner
    {
        public IEnumerable<DatabasePackage> GetPackages()
        {
            return Enumerable.Empty<DatabasePackage>();
        }
    }
}