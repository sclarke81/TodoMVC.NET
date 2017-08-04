using Prism.Events;

namespace Infrastructure
{
    public enum Filter
    {
        Active,
        Completed,
        All
    }

    public class ToDoListChangedPayload
    {
        public enum ChangeAction
        {
            Add,
            ChangeState,
            ClearActive
        };
        public ChangeAction Action { get; set; }
        public ToDo ToDo { get; set; }
    }

    public class ToDoListChanged : PubSubEvent<ToDoListChangedPayload> { };
    public class FilterEvent : PubSubEvent<Filter> {}
}
