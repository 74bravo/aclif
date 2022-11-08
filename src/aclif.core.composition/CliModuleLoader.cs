using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition.Primitives;
using System.IO;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;
using aclif.core.interfaces;

namespace aclif
{
    public sealed class CliModuleLoader : IDisposable
    {
        private readonly AggregateCatalog? _catalog;
        private readonly CompositionContainer? _container;
        [Import(typeof(ICliModuleCollection))]
        private ICliModuleCollection _cliModuleCollection;

        [Import(typeof(ICliShellInterface))]
        private ICliShellInterface _cliShellInterface;

        internal class DistinctAggregateCatalog : AggregateCatalog
        {

            class PartDefinitionComparer : IEqualityComparer<ComposablePartDefinition>
            {
                public bool Equals(ComposablePartDefinition? x, ComposablePartDefinition? y)
                    => x != null && y != null &&  x.ToString() == y.ToString();
                public int GetHashCode(ComposablePartDefinition? obj)
                {
                    return obj == null ? string.Empty.GetHashCode() : obj.ToString().GetHashCode();
                }
            }

            class PartDefinitionTupleComparer : IEqualityComparer<Tuple<ComposablePartDefinition, ExportDefinition>>
            {
                public bool Equals(Tuple<ComposablePartDefinition, ExportDefinition>? x
                                 , Tuple<ComposablePartDefinition, ExportDefinition>? y)
                    => x?.Item1 != null && y?.Item1 != null && x.Item1.ToString() == y.Item1.ToString();
                public int GetHashCode([DisallowNull] Tuple<ComposablePartDefinition, ExportDefinition> obj)
                    => obj?.Item1 == null ? string.Empty.GetHashCode() : obj.Item1.ToString().GetHashCode();
            }

            public override IQueryable<ComposablePartDefinition> Parts => base.Parts.Distinct(new PartDefinitionComparer());

            public override IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> GetExports(ImportDefinition definition)
            {
                var baseresult = base.GetExports(definition).Distinct(new PartDefinitionTupleComparer());
                return baseresult;
            }

        }


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.
            public CliModuleLoader(string cliModuleSearchString = "*.dll")

        {
            this._catalog = new DistinctAggregateCatalog();

            Assembly assembly = typeof(CliModuleLoader).Assembly;

            this._catalog.Catalogs.Add((ComposablePartCatalog)new AssemblyCatalog(assembly));

            this._catalog.Catalogs.Add((ComposablePartCatalog)new DirectoryCatalog(Path.GetDirectoryName(assembly.Location), cliModuleSearchString));

            this._container = new CompositionContainer((ComposablePartCatalog)this._catalog, Array.Empty<ExportProvider>());
            try
            {
                this._container.ComposeParts((object)this);
            }
            catch (ReflectionTypeLoadException ex)
            {
                Console.WriteLine(ex.Message);
                if (ex.LoaderExceptions == null || ex.LoaderExceptions.Length == 0)
                    return;
                Console.WriteLine("ModuleLoader - Exception List:");

                foreach (Exception loaderException in ex.LoaderExceptions)

                    Console.WriteLine("\t" + loaderException.Message);


                Console.WriteLine("************************************");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ModuleLoader - ComposeParts Exception : " + ex.Message);
            }
        }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8604 // Possible null reference argument.

        public ICliModuleCollection Collection => this._cliModuleCollection;

        public ICliShellInterface ShellInterface => this._cliShellInterface;

        public void Dispose()
        {
            this._catalog?.Dispose();
            this._container?.Dispose();
            GC.SuppressFinalize((object)this);
        }



    }
}
