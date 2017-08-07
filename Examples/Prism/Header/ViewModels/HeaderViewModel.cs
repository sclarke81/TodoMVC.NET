using Infrastructure;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Header.ViewModels
{
    public class HeaderViewModel : BindableBase
    {
        public string TaskName
        {
            get;
            set;
        }
        
        public ICommand AddTaskCommand { get; set; }

        public ICommand SelectAllCommand { get; set; }

        public HeaderViewModel(IModel model, IEventAggregator eventAggregator)
        {
            AddTaskCommand = new DelegateCommand(() => {
                var newTodo = new ToDo() { Name = TaskName, State = false };
                model.ToDos.Add(newTodo);
                eventAggregator.GetEvent<Infrastructure.ToDoListChanged>().Publish();
            });

            SelectAllCommand = new DelegateCommand(() =>
            {
                bool bAllSelected = true;
                foreach (var todo in model.ToDos)
                {
                    if (todo.State == false)
                    {
                        bAllSelected = false;
                        break;
                    }
                }
                foreach (var todo in model.ToDos)
                {
                    todo.State = !bAllSelected;
                }
                eventAggregator.GetEvent<Infrastructure.ToDoListChanged>().Publish();
            });
        }
    }
}
