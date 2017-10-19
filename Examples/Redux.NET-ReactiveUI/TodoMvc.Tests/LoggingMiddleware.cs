using System;
using System.Collections.Generic;
using Redux;

namespace TodoMvc.Tests
{
    public class LoggingMiddleware
    {
        public List<IAction> LoggedActions { get; private set; } = new List<IAction>();

        public Func<Dispatcher, Dispatcher> Middleware<TState>(IStore<TState> store)
        {
            return (Dispatcher next) => (IAction action) =>
            {
                LoggedActions.Add(action);
                return next(action);
            };
        }
    }
}
