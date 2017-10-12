using Redux;
using System;
using System.Collections.Immutable;
using System.Linq;
using TodoMvc.States;

namespace TodoMvc
{
    public static class Reducers
    {
        public static ApplicationState ReduceApplication(ApplicationState previousState, IAction action)
        {
            return new ApplicationState
            {
                Filter = FilterChanged(previousState.Filter, action),
                Todos = TodosReducer(previousState.Todos, action)
            };
        }

        private static TodosFilter FilterChanged(TodosFilter previousState, IAction action)
        {
            return action is FilterChangedAction ? ((FilterChangedAction)action).Filter : previousState;
        }

        private static ImmutableArray<Todo> TodosReducer(ImmutableArray<Todo> previousState, IAction action)
        {

            if (action is TodoAddedAction addTodoAction)
            {
                return TodoAdded(previousState, addTodoAction);
            }

            if (action is TodoDeletedAction deleteTodoAction)
            {
                return TodoDeleted(previousState, deleteTodoAction);
            }

            if (action is TodoEditedAction editTodoAction)
            {
                return TodoEdited(previousState, editTodoAction);
            }

            if (action is CompletedTodosClearedAction)
            {
                return CompletedTodosCleared(previousState);
            }

            if (action is AllTodosIsCompletedChangedAction allTodosCompletedAction)
            {
                return AllTodosIsCompletedChanged(previousState, allTodosCompletedAction);
            }

            if (action is TodoIsCompletedChangedAction completeTodoAction)
            {
                return TodoIsCompletedChanged(previousState, completeTodoAction);
            }

            return previousState;
        }

        private static ImmutableArray<Todo> TodoAdded(ImmutableArray<Todo> previousState, TodoAddedAction action)
        {
            ImmutableArray<Todo> newState;

            string trimmedTitle = action.Text.Trim();

            if (string.IsNullOrWhiteSpace(trimmedTitle))
            {
                newState = previousState;
            }
            else
            {
                newState = previousState
                    .Insert(0, new Todo
                    {
                        Id = Guid.NewGuid(),
                        Title = trimmedTitle,
                        IsCompleted = false
                    });
            }

            return newState;
        }

        private static ImmutableArray<Todo> TodoDeleted(ImmutableArray<Todo> previousState, TodoDeletedAction action)
        {
            Todo todoToDelete = previousState.First(todo => todo.Id == action.Id);

            return previousState.Remove(todoToDelete);
        }

        private static ImmutableArray<Todo> TodoEdited(ImmutableArray<Todo> previousState, TodoEditedAction action)
        {
            Todo todoToEdit = previousState.First(todo => todo.Id == action.Id);

            return previousState
                .Replace(todoToEdit, new Todo
                {
                    Id = todoToEdit.Id,
                    Title = action.Text,
                    IsCompleted = todoToEdit.IsCompleted
                });
        }

        private static ImmutableArray<Todo> CompletedTodosCleared(ImmutableArray<Todo> previousState)
        {
            return previousState.RemoveAll(todo => todo.IsCompleted);
        }

        private static ImmutableArray<Todo> AllTodosIsCompletedChanged(ImmutableArray<Todo> previousState, AllTodosIsCompletedChangedAction action)
        {
            return previousState
                .Select(x => new Todo
                {
                    Id = x.Id,
                    Title = x.Title,
                    IsCompleted = action.IsCompleted
                })
                .ToImmutableArray();
        }

        private static ImmutableArray<Todo> TodoIsCompletedChanged(ImmutableArray<Todo> previousState, TodoIsCompletedChangedAction action)
        {
            Todo todoToEdit = previousState.First(todo => todo.Id == action.Id);

            return previousState
                .Replace(todoToEdit, new Todo
                {
                    Id = todoToEdit.Id,
                    Title = todoToEdit.Title,
                    IsCompleted = action.IsCompleted
                });
        }
    }
}
