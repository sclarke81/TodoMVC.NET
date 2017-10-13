using TodoMvc;
using System.Collections.Generic;
using System.Linq;
using TodoMvc.ViewModels;
using TodoMvc.States;

namespace TodoMvc
{
    public static class Selectors
    {
        public static ApplicationViewModel MakeApplicationViewModel(ApplicationState state)
        {
            return new ApplicationViewModel
            {
                MainAndFooterAreVisible = state.Todos.Any()
            };
        }
    }
}
