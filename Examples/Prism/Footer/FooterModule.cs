using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Modularity;
using Prism.Regions;
using Prism.Mvvm;
using Microsoft.Practices.Unity;

namespace Footer
{
    public class FooterModule : IModule
    {
        private readonly IRegionManager regionManager;
        public FooterModule(IRegionManager regionManager, IUnityContainer container)
        {
            this.regionManager = regionManager;
            //Im actually not sure if this is the right place to do this.. I couldn't find any advice on the internet
            //when I looked. I suspect this should go in initialise.
            ViewModelLocationProvider.Register(typeof(Footer).ToString(), () => container.Resolve<FooterViewModel>());
        }

        public void Initialize()
        {
            //here we register controls with regions set in the shell.
            regionManager.RegisterViewWithRegion("Footer", typeof(Footer));
        }
    }
}
