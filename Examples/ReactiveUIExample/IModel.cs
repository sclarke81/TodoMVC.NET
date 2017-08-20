using System;
using ReactiveUI;
using ReactiveUIExample.Enums;

namespace ReactiveUIExample
{
    public interface IModel
    {
        IReactiveList<TodoViewModel> ToDos { get; }

        IObservable<Enums.EFilter> Filter { get; }

        void UpdateFilter(Enums.EFilter filter);
    }
}