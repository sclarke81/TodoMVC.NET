using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Controls;
using ReactiveUI;
using Redux;
using TodoMvc.States;
using TodoMvc.Store;

namespace TodoMvc.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        public ReactiveCommand TodoCompletedChanged { get; }
        public ReactiveCommand TodoDeleted { get; }

        private readonly ObservableAsPropertyHelper<IEnumerable<Todo>> _todos;
        public IEnumerable<Todo> Todos => _todos.Value;

        public MainViewModel(IStore<ApplicationState> store)
        {
            TodoCompletedChanged = ReactiveCommand.Create<CheckBox>(checkBox =>
            {
                store.Dispatch(new TodoIsCompletedChangedAction() { Id = (Guid)checkBox.Tag, IsCompleted = checkBox.IsChecked.Value });
            });

            TodoDeleted = ReactiveCommand.Create<Guid>(id => store.Dispatch(new TodoDeletedAction() { Id = id }));

            store.Select(x => FilterTodos(x.Todos, x.Filter))
                 .DistinctUntilChanged()
                 .ToProperty(this, x => x.Todos, out _todos);
        }

        private static IEnumerable<Todo> FilterTodos(IEnumerable<Todo> todos, TodosFilter filter)
        {
            IEnumerable<Todo> result;

            switch (filter)
            {
                case TodosFilter.Active:
                {
                    result = todos.Where(x => !x.IsCompleted);
                    break;
                }
                case TodosFilter.Completed:
                {
                    result = todos.Where(x => x.IsCompleted);
                    break;
                }
                case TodosFilter.All:
                default:
                {
                    result = todos;
                }
                break;
            }

            return result;
        }
    }
}
