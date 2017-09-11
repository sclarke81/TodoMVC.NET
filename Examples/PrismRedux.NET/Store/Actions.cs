using System;
using Redux;
using PrismRedux.NET.StoreNS.States;

namespace PrismRedux.NET.StoreNS
{
    public class TodoAddedAction : IAction
    {
        public string Text { get; set; }
    }

    public class TodoDeletedAction : IAction
    {
        public Guid Id { get; set; }
    }

    public class TodoEditedAction : IAction
    {
        public string Text { get; set; }
        public Guid Id { get; set; }
    }

    public class TodoIsCompletedChangedAction : IAction
    {
        public Guid Id { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class AllTodosIsCompletedChangedAction : IAction
    {
        public bool IsCompleted { get; set; }
    }

    public class CompletedTodosClearedAction : IAction { }

    public class FilterChangedAction : IAction
    {
        public TodosFilter Filter { get; set; }
    }
}
