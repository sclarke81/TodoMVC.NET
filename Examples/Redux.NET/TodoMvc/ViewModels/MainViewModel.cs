using System.Collections.Generic;
using TodoMvc.States;

namespace TodoMvc.ViewModels
{
    public class MainViewModel
    {
        public IEnumerable<Todo> Todos { get; set; }
    }
}
