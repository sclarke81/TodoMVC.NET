using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Modularity;
using Prism.Regions;
using Prism.Mef.Modularity;
using System.ComponentModel.Composition;

namespace PrismRedux.NET.Header
{
    [ModuleExport(typeof(HeaderModule))]
    public class HeaderModule : IModule
    {
        private readonly IRegionManager regionManager;
        [ImportingConstructor]
        public HeaderModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }
        public void Initialize()
        {
            regionManager.RegisterViewWithRegion("Header", typeof(Header));
        }
    }
}
