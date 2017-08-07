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
using System.ComponentModel;
using System.Windows.Data;

namespace Main.ViewModels
{
    /// <summary>
    /// BindableBase is a prism class that basically implements INotifyPropertyChanged.
    /// </summary>
    public class MainViewModel : BindableBase
    {
        private Infrastructure.Filter _filter = Filter.All;
        public ICollectionView VisibleItems { get; set; }
        public ICommand ToDoStateChangedCommand { get; set; }
        public ICommand DeleteItemCommand { get; set; }
        private bool FilterPredicate(object obj)
        {
            bool retval = false;
            var todo = (ToDo)obj;
            switch (_filter)
            {
                case Filter.Active:
                    retval = todo.State == false;
                    break;
                case Filter.Completed:
                    retval = todo.State == true;
                    break;
                default:
                    retval = true;
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
            VisibleItems = CollectionViewSource.GetDefaultView(model.ToDos);
            VisibleItems.Filter = new Predicate<object>(FilterPredicate);
            //Delegate command is basically a helper function you specify a delegate to run with.
            ToDoStateChangedCommand = new DelegateCommand<ToDo>((todo) => {
                eventAggregator.GetEvent<ToDoListChanged>()
                .Publish();
                VisibleItems.Refresh();
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
                    VisibleItems.Refresh();
                });
            eventAggregator.GetEvent<ToDoListChanged>()
                .Subscribe(() => {
                    VisibleItems.Refresh();
                });
        }
    }
}
