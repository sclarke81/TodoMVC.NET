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

        public static HeaderViewModel MakeHeaderViewModel(ApplicationState state)
        {
            return new HeaderViewModel
            {
                MarkAllAsCompleteIsChecked = state.Todos.All(x => x.IsCompleted),
                MarkAllAsCompleteIsVisible = state.Todos.Any()
            };
        }
    }
}
