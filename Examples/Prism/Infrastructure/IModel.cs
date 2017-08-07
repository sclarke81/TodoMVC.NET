using System.Collections.Generic;

namespace Infrastructure
{
    public interface IModel
    {
        IList<ToDo> ToDos { get; }
    }
}
