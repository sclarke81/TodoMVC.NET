using System.Collections.Immutable;

namespace PrismRedux.NET.StoreNS.States
{
    public class ApplicationState
    {
        public ImmutableArray<Todo> Todos { get; set; }

        public TodosFilter Filter { get; set; }
    }
}
