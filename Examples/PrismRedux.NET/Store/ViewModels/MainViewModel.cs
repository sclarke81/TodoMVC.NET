using System.Collections.Generic;
using PrismRedux.NET.StoreNS.States;

namespace PrismRedux.NET.StoreNS.ViewModels
{
    public class MainViewModel
    {
        public IEnumerable<Todo> Todos { get; set; }
    }
}
