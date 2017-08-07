using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Modularity;
using Prism.Regions;

namespace Header
{
    public class HeaderModule : IModule
    {
        private readonly IRegionManager regionManager;
        public HeaderModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }
        public void Initialize()
        {
            regionManager.RegisterViewWithRegion("Header", typeof(Views.Header));
        }
    }
}
