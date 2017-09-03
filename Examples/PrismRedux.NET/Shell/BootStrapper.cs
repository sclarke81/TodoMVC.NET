using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Mef;
using Prism.Modularity;
using PrismRedux.NET.StoreNS;
using PrismRedux.NET.StoreNS.States;
using Redux;

namespace PrismRedux.NET.Shell
{
    public class BootStrapper : MefBootstrapper
    {
        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new DirectoryModuleCatalog() { ModulePath = "..\\..\\..\\Modules" };
        }

        protected override void ConfigureContainer()
        {
            this.RegisterBootstrapperProvidedTypes();
        }

        protected override void RegisterBootstrapperProvidedTypes()
        {
            base.RegisterBootstrapperProvidedTypes();
            this.Container.ComposeExportedValue<IStore<ApplicationState>>(
                new Store<ApplicationState>(Reducers.ReduceApplication, new ApplicationState()
                {
                    Filter = TodosFilter.All,
                    Todos = ImmutableArray.Create<Todo>()
                })
            );
        }

        protected override DependencyObject CreateShell()
        {
            return new Shell();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow = (PrismRedux.NET.Shell.Shell)Shell;
            Application.Current.MainWindow.Show();
        }
    }
}
