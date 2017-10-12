﻿using TodoMvc;
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

        public static MainViewModel MakeMainViewModel(ApplicationState state)
        {
            IEnumerable<Todo> todos;

            switch (state.Filter)
            {
                case TodosFilter.Active:
                {
                    todos = state.Todos.Where(x => !x.IsCompleted);
                    break;
                }
                case TodosFilter.Completed:
                {
                    todos = state.Todos.Where(x => x.IsCompleted);
                    break;
                }
                case TodosFilter.All:
                default:
                {
                    todos = state.Todos;
                }
                break;
            }

            return new MainViewModel() { Todos = todos };
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
