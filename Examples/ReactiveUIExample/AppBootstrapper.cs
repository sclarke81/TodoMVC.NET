using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Splat;

namespace ReactiveUIExample
{
    public class AppBootstrapper
    {
        private IModel _model = new Model();
        public AppBootstrapper()
        {
            Locator.CurrentMutable.Register(() => _model, typeof(IModel));
            Locator.CurrentMutable.Register(() => new Main(), typeof(IViewFor<MainViewModel>));
            Locator.CurrentMutable.Register(() => new Header(), typeof(IViewFor<HeaderViewModel>));
            Locator.CurrentMutable.Register(() => new Footer(), typeof(IViewFor<FooterViewModel>));
        }
    }
}
