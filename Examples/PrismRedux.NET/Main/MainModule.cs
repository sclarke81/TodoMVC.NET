using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mef.Modularity;
using Prism.Modularity;
using Prism.Regions;

namespace Main
{
    [ModuleExport(typeof(MainModule))]
    public class MainModule : IModule
    {
        private readonly IRegionManager regionManager;
        [ImportingConstructor]
        public MainModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }
        public void Initialize()
        {
            regionManager.RegisterViewWithRegion("Main", typeof(Main));
        }
    }
}
