using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;
using ReactiveUI;
using Redux;
using TodoMvc.States;
using TodoMvc.Store;

namespace TodoMvc.ViewModels
{
    public class FooterViewModel : ReactiveObject
    {
        public ReactiveCommand ClearCompleted { get; }
        public ReactiveCommand FilterChanged { get; }

        private readonly ObservableAsPropertyHelper<Visibility> _clearCompletedVisibility;
        public Visibility ClearCompletedVisibility => _clearCompletedVisibility.Value;

        private readonly ObservableAsPropertyHelper<string> _counterText;
        public string CounterText => _counterText.Value;

        private readonly ObservableAsPropertyHelper<bool> _filterActiveIsChecked;
        public bool FilterActiveIsChecked => _filterActiveIsChecked.Value;

        private readonly ObservableAsPropertyHelper<bool> _filterCompletedIsChecked;
        public bool FilterCompletedIsChecked => _filterCompletedIsChecked.Value;

        private readonly ObservableAsPropertyHelper<bool> _filterAllIsChecked;
        public bool FilterAllIsChecked => _filterAllIsChecked.Value;

        public FooterViewModel(IStore<ApplicationState> store)
        {
            ClearCompleted = ReactiveCommand.Create(() => store.Dispatch(new CompletedTodosClearedAction()));
            FilterChanged = ReactiveCommand.Create<TodosFilter>(param => store.Dispatch(new FilterChangedAction() { Filter = param }));

            store.Select(s => s.Todos.Any(todo => todo.IsCompleted) ? Visibility.Visible : Visibility.Hidden)
                 .DistinctUntilChanged()
                 .ToProperty(this, x => x.ClearCompletedVisibility, out _clearCompletedVisibility);

            store.Select(s => GetCounterMessage(s.Todos))
                 .DistinctUntilChanged()
                 .ToProperty(this, x => x.CounterText, out _counterText);

            store.Select(s => s.Filter == TodosFilter.Active)
                 .DistinctUntilChanged()
                 .ToProperty(this, x => x.FilterActiveIsChecked, out _filterActiveIsChecked);

            store.Select(s => s.Filter == TodosFilter.Completed)
                 .DistinctUntilChanged()
                 .ToProperty(this, x => x.FilterCompletedIsChecked, out _filterCompletedIsChecked);

            store.Select(s => s.Filter == TodosFilter.All)
                 .DistinctUntilChanged()
                 .ToProperty(this, x => x.FilterAllIsChecked, out _filterAllIsChecked);
        }

        private static string GetCounterMessage(IEnumerable<Todo> todos)
        {
            int activeTodoCount = todos.Count(todo => !todo.IsCompleted);
            string itemWord = activeTodoCount <= 1 ? "item" : "items";
            return $"{activeTodoCount} {itemWord} left";
        }
    }
}
