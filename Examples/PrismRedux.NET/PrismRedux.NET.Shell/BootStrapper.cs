using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Mef;
using Prism.Modularity;

namespace PrismRedux.NET.Shell
{
    public class BootStrapper : MefBootstrapper
    {
        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new DirectoryModuleCatalog() { ModulePath = "..\\..\\..\\Modules" };
        }

        protected override DependencyObject CreateShell()
        {
            return new PrismRedux.NET.Shell.Shell();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow = (PrismRedux.NET.Shell.Shell)Shell;
            Application.Current.MainWindow.Show();
        }
    }
}
