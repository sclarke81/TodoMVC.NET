using PrismRedux.NET.StoreNS;
using System.Collections.Generic;
using System.Linq;
using PrismRedux.NET.StoreNS.ViewModels;
using PrismRedux.NET.StoreNS.States;

namespace PrismRedux.NET.StoreNS
{
    public static class Selectors
    {
        public static ApplicationViewModel MakeApplicationViewModel(ApplicationState state)
        {
            return new ApplicationViewModel
            {
                MainAndFooterAreVisible = state.Todos.Any()
            };
        }

        public static MainViewModel MakeMainViewModel(ApplicationState state)
        {
            IEnumerable<Todo> todos;

            switch (state.Filter)
            {
                case TodosFilter.Active:
                {
                    todos = state.Todos.Where(x => !x.IsCompleted);
                    break;
                }
                case TodosFilter.Completed:
                {
                    todos = state.Todos.Where(x => x.IsCompleted);
                    break;
                }
                case TodosFilter.All:
                default:
                {
                    todos = state.Todos;
                }
                break;
            }

            return new MainViewModel() { Todos = todos };
        }

        public static HeaderViewModel MakeHeaderViewModel(ApplicationState state)
        {
            return new HeaderViewModel
            {
                MarkAllAsCompleteIsChecked = state.Todos.All(x => x.IsCompleted),
                MarkAllAsCompleteIsVisible = state.Todos.Any()
            };
        }

        public static FooterViewModel MakeFooterViewModel(ApplicationState state)
        {
            return new FooterViewModel
            {
                ClearCompletedIsVisible = state.Todos.Any(todo => todo.IsCompleted),
                CounterText = GetCounterMessage(state.Todos),
                SelectedFilter = state.Filter,
             };
        }

        private static string GetCounterMessage(IEnumerable<Todo> todos)
        {
            int activeTodoCount = todos.Count(todo => !todo.IsCompleted);
            string itemWord = activeTodoCount <= 1 ? "item" : "items";
            return $"{activeTodoCount} {itemWord} left";
        }
    }
}
