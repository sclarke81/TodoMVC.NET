using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Splat;

namespace ReactiveUIExample
{
    public class ShellViewModel
    {
        public MainViewModel MainViewModel
        {
            get;
            set;
        }

        public HeaderViewModel HeaderViewModel
        {
            get;
            set;
        }

        public FooterViewModel FooterViewModel
        {
            get;
            set;
        }

        public ShellViewModel()
        {
            var model = (IModel)Locator.CurrentMutable.GetService(typeof(IModel));
            MainViewModel = new MainViewModel(model);
            HeaderViewModel = new HeaderViewModel(model);
            FooterViewModel = new FooterViewModel(model);
            //This needs to be fired after all the handlers have been wired up so do it here.
            //I would actually prefer this to be fired in the application code behind after the views have been loaded
            //but there doesn't seem to be an event for this circumstance.
            model.UpdateFilter(Enums.EFilter.All);
        }
    }
}
