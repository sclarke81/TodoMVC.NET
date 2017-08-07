using Prism.Events;

namespace Infrastructure
{
    public enum Filter
    {
        Active,
        Completed,
        All
    }

    /// <summary>
    /// Reflecting changes in a list in the model is actually quite a difficult problem. The solution I have implemented here is hardly 
    /// an adequate solution for the general case. In this case, if any change is made to the list in the model, the viewmodels must traverse the entire
    /// list to find any changes and update them in the view. With large lists, I can see this causing performance issues.
    /// </summary>
    public class ToDoListChanged : PubSubEvent { };
    public class FilterEvent : PubSubEvent<Filter> {}
}
