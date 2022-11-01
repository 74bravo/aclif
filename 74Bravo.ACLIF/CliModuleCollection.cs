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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0044:Add readonly modifier", Justification = "<Pending>")]
        private IEnumerable<Lazy<ICliModule>> _moduleDefinitions;
        private readonly IReadOnlyList<ICliModule> _explicitModules;

        public CliModuleCollection() => this._explicitModules = (IReadOnlyList<ICliModule>)null;

        public CliModuleCollection(IEnumerable<ICliModule> explicitModules) => this._explicitModules = (IReadOnlyList<ICliModule>)explicitModules.ToArray<ICliModule>();

        public IEnumerable<ICliModule> CliModules => (IEnumerable<ICliModule>)this._explicitModules ?? this._moduleDefinitions.Select<Lazy<ICliModule>, ICliModule>((Func<Lazy<ICliModule>, ICliModule>)(x => x.Value));

    }
}
