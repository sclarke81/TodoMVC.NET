using System.Linq;
using System.Reactive.Linq;
using System.Windows;
using ReactiveUI;
using Redux;
using TodoMvc.States;
using System;
using TodoMvc.Store;

namespace TodoMvc.ViewModels
{
    public class HeaderViewModel : ReactiveObject
    {
        public ReactiveCommand TodoAdded { get; }
        public ReactiveCommand MarkAllAsComplete { get; }

        private readonly ObservableAsPropertyHelper<Visibility> _markAllAsCompleteVisibility;
        public Visibility MarkAllAsCompleteVisibility => _markAllAsCompleteVisibility.Value;

        private readonly ObservableAsPropertyHelper<bool> _markAllAsCompleteIsChecked;
        public bool MarkAllAsCompleteIsChecked => _markAllAsCompleteIsChecked.Value;

        public HeaderViewModel(IStore<ApplicationState> store)
        {
            TodoAdded = ReactiveCommand.Create<string>(title => store.Dispatch(new TodoAddedAction() { Text = title }));

            MarkAllAsComplete = ReactiveCommand.Create<bool>(isChecked => store.Dispatch(new AllTodosIsCompletedChangedAction()
            {
                IsCompleted = isChecked
            }));

            store.Select(s => s.Todos.Any() ? Visibility.Visible : Visibility.Hidden)
                 .DistinctUntilChanged()
                 .ToProperty(this, x => x.MarkAllAsCompleteVisibility, out _markAllAsCompleteVisibility);

            store.Select(s => s.Todos.Any() && s.Todos.All(x => x.IsCompleted))
                 .DistinctUntilChanged()
                 .ToProperty(this, x => x.MarkAllAsCompleteIsChecked, out _markAllAsCompleteIsChecked);
        }
    }
}
