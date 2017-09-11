using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mef.Modularity;
using Prism.Modularity;
using Prism.Regions;

namespace Footer
{
    [ModuleExport(typeof(FooterModule))]
    public class FooterModule : IModule
    {
        private readonly IRegionManager regionManager;
        [ImportingConstructor]
        public FooterModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }
        public void Initialize()
        {
            regionManager.RegisterViewWithRegion("Footer", typeof(Footer));
        }
    }
}
