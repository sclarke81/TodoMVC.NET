using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace ReactiveUIExample
{
    public class MainViewModel : ReactiveObject
    {
        private IReactiveDerivedList<TodoViewModel> _visibleItems;
        public IReactiveDerivedList<TodoViewModel> VisibleItems
        {
            get { return _visibleItems; }
            set { this.RaiseAndSetIfChanged(ref _visibleItems, value); }
        }

        public ReactiveCommand<TodoViewModel, Unit> DeleteItemCommand { get; private set; }

        public MainViewModel(IModel model)
        {
            bool filterFunc(Enums.EFilter filter, bool state)
            {
                bool retval = true;
                switch (filter)
                {
                    case Enums.EFilter.Active:
                        retval = !state;
                        break;
                    case Enums.EFilter.Completed:
                        retval = state;
                        break;
                    default:
                        retval = true;
                        break;
                }
                return retval;
            };
            model.Filter.Subscribe(filter => {
                VisibleItems = model.ToDos.CreateDerivedCollection(todo => todo, todo => filterFunc(filter, todo.State));
            });

            DeleteItemCommand = ReactiveCommand.Create<TodoViewModel>(todo => 
                model.ToDos.Remove(todo)
            );
        }
    }
}
