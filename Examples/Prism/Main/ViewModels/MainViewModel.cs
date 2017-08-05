using Infrastructure;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Main.ViewModels
{
    /// <summary>
    /// BindableBase is a prism class that basically implements INotifyPropertyChanged.
    /// </summary>
    public class MainViewModel : BindableBase
    {
        private Infrastructure.Filter _filter = Filter.All;
        private IEnumerable<ToDo> _visibleItems;
        public IEnumerable<ToDo> VisibleItems
        {
            get { return _visibleItems;  }
            set { SetProperty(ref _visibleItems, value); }
        }

        public ICommand ToDoStateChangedCommand { get; set; }

        public ICommand DeleteItemCommand { get; set; }

        private IEnumerable<ToDo> FilterItems(Filter filter, IEnumerable<ToDo> toFilter)
        {
            IEnumerable<ToDo> retval = null;
            switch (filter)
            {
                case Filter.Active:
                    retval = toFilter.Where(todo => todo.State == false);
                    break;
                case Filter.Completed:
                    retval = toFilter.Where(todo => todo.State == true);
                    break;
                default:
                    retval = toFilter.ToList();
                    break;
            }
            return retval;
        }

        /// <summary>
        /// In the constructor I have passed in interfaces for IModel and IEventAggregator. the viewmodellocator
        /// uses the IOC container to get the class instances, and inject them into the viewmodel.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="eventAggregator"></param>
        public MainViewModel(IModel model, IEventAggregator eventAggregator)
        {
            //Delegate command is basically a helper function you specify a delegate to run with.
            ToDoStateChangedCommand = new DelegateCommand<ToDo>((todo) => {
                eventAggregator.GetEvent<ToDoListChanged>()
                .Publish();
                VisibleItems = FilterItems(_filter, model.ToDos);
            });
            DeleteItemCommand = new DelegateCommand<ToDo>((todo) => {
                model.ToDos.Remove(todo);
                eventAggregator.GetEvent<ToDoListChanged>()
                .Publish();
            });
            //Any module can subscribe to or publish events on the event aggregator. The events themselves are
            //defined in a shared infrastructure project. The eventaggregator is responsible for telling the 
            //various components to update themselves.
            eventAggregator.GetEvent<Infrastructure.FilterEvent>()
                .Subscribe(filter => {
                    _filter = filter;
                    VisibleItems = FilterItems(_filter, model.ToDos);
                });
            eventAggregator.GetEvent<ToDoListChanged>()
                .Subscribe(() => {
                    VisibleItems = FilterItems(_filter, model.ToDos);
                });
        }
    }
}
