using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ACLIF
{
   
    [Export(typeof(ICliModuleCollection))]
    internal class CliModuleCollection : ICliModuleCollection
    {
        [ImportMany]

#pragma warning disable CS0649 // values initialized via [ImportMany]
#pragma warning disable CS8618 // values initialized via [ImportMany]
        private IEnumerable<Lazy<ICliModule>> _moduleDefinitions;
        private readonly IReadOnlyList<ICliModule> _explicitModules;


#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        public CliModuleCollection() => this._explicitModules = (IReadOnlyList<ICliModule>)null;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

        public CliModuleCollection(IEnumerable<ICliModule> explicitModules) => this._explicitModules = (IReadOnlyList<ICliModule>)explicitModules.ToArray<ICliModule>();

#pragma warning restore CS0649 // values initialized via [ImportMany]
#pragma warning restore CS8618 // values initialized via [ImportMany]

        public IEnumerable<ICliModule> CliModules => (IEnumerable<ICliModule>)this._explicitModules ?? this._moduleDefinitions.Select<Lazy<ICliModule>, ICliModule>((Func<Lazy<ICliModule>, ICliModule>)(x => x.Value));

    }
}
