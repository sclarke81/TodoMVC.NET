using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace ReactiveUIExample
{
    public class Model : IModel
    {
        public IReactiveList<TodoViewModel> ToDos
        {
            get;
            private set;
        } = new ReactiveList<TodoViewModel>() { ChangeTrackingEnabled = true };

        private Subject<Enums.EFilter> _filter = new Subject<Enums.EFilter>();

        public IObservable<Enums.EFilter> Filter => _filter.AsObservable();

        public void UpdateFilter(Enums.EFilter filter)
        {
            _filter.OnNext(filter);
        }
    }
}
