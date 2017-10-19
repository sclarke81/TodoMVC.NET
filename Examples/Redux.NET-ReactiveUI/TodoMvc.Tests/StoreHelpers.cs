using System;
using System.Collections.Immutable;
using Redux;
using TodoMvc.States;
using TodoMvc.Store;

namespace TodoMvc.Tests
{
    public static class StoreHelpers
    {
        public static Store<ApplicationState> GetStore()
        {
            return new Store<ApplicationState>(Reducers.ReduceApplication, new ApplicationState()
            {
                Filter = TodosFilter.All,
                Todos = ImmutableArray.Create<Todo>()
            });
        }

        public static Store<ApplicationState> AddTodo(this Store<ApplicationState> currentStore, string title, Guid id, bool isCompleted)
        {
            ApplicationState state = currentStore.GetState();
            state.Todos = state.Todos.Add(new Todo
            {
                Title = title,
                Id = id,
                IsCompleted = isCompleted
            });

            return new Store<ApplicationState>(Reducers.ReduceApplication, state);
        }
    }
}
