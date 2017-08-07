using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Infrastructure
{
    /// <summary>
    /// The model of the todo list is simply a basic list. Notice how the model is not responsible for 
    /// telling the view to update. Requests to update are handled within the viewmodels via the
    /// eventaggregator.
    /// </summary>
    public class Model : IModel
    {
        public IList<ToDo> ToDos { get; private set; } = new List<ToDo>();
    }
}
