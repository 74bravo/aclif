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

namespace ACLIF
{
    internal sealed class CliModuleLoader : IDisposable
    {
        private readonly AggregateCatalog? _catalog;
        private readonly CompositionContainer? _container;
        [Import(typeof(ICliModuleCollection))]
        private ICliModuleCollection _cliModuleCollection;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.
        public CliModuleLoader(string cliModuleSearchString = "*.climodule.*.dll")

        {
            this._catalog = new AggregateCatalog();
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

        public void Dispose()
        {
            this._catalog?.Dispose();
            this._container?.Dispose();
            GC.SuppressFinalize((object)this);
        }



    }
}
