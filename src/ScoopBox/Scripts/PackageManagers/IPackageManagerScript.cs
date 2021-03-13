using System.Collections.Generic;

namespace ScoopBox
{
    /// <summary>
    /// Represents a package manager script. This script is generated automatically based on user input.
    /// </summary>
    public interface IPackageManagerScript : IScript
    {
        /// <summary>
        /// Gets the applications that will be installed using this package manager.
        /// </summary>
        IEnumerable<string> Applications { get; }
    }
}
