using Infrastructure;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Footer
{
    public class FooterViewModel : BindableBase
    {
        public FooterViewModel(IModel model, IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<ToDoListChanged>().Subscribe((payload) => {
                RemainingTaskCount = model.ToDos.Where((td) => td.State == false)
                .Count()
                .ToString();
            });
            ShowActiveCommand = new DelegateCommand<object>((o) => 
            {
                eventAggregator.GetEvent<Infrastructure.FilterEvent>().Publish(Filter.Active);
            });
            ShowCompletedCommand = new DelegateCommand(() =>
            {
                eventAggregator.GetEvent<Infrastructure.FilterEvent>().Publish(Filter.Completed);
            });
            ShowAllCommand = new DelegateCommand(() =>
            {
                eventAggregator.GetEvent<Infrastructure.FilterEvent>().Publish(Filter.All);
            });
            ClearAllCommand = new DelegateCommand(() =>
            {
                IEnumerable<ToDo> remainingToDos = model.ToDos
                    .Where(todo => todo.State == false)
                    .ToList();
                model.ToDos.Clear();
                foreach (var todo in remainingToDos)
                {
                    model.ToDos.Add(todo);
                }
                eventAggregator.GetEvent<ToDoListChanged>().Publish(
                    new ToDoListChangedPayload()
                    {
                        Action = ToDoListChangedPayload.ChangeAction.ClearActive,
                        ToDo = null
                    }
                    );
            });
        }

        public ICommand ShowActiveCommand { get; set; }
        public ICommand ShowCompletedCommand { get; set; }
        public ICommand ShowAllCommand { get; set; }
        public ICommand ClearAllCommand { get; set; }

        private string _remainingTaskCount;
        public string RemainingTaskCount
        {
            get { return _remainingTaskCount; }
            set { SetProperty(ref _remainingTaskCount, value); }
        }
    }
}
