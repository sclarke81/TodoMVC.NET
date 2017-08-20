using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace ReactiveUIExample
{
    public class FooterViewModel : ReactiveObject
    {
        private readonly ObservableAsPropertyHelper<int> _numberOfActiveItems;
        public int NumberOfActiveItems => _numberOfActiveItems.Value;
        public ReactiveCommand ShowAll { get; private set; }
        public ReactiveCommand ShowCompleted { get; private set; }
        public ReactiveCommand ShowActive { get; private set; }
        private ObservableAsPropertyHelper<bool> _isAllChecked;
        private ObservableAsPropertyHelper<bool> _isCompletedChecked;
        private ObservableAsPropertyHelper<bool> _isActiveChecked;
        public bool IsAllChecked => _isAllChecked.Value;
        public bool IsCompletedChecked => _isCompletedChecked.Value;
        public bool IsActiveChecked => _isActiveChecked.Value;
        public ReactiveCommand ClearSelected { get; private set; }

        public FooterViewModel(IModel model)
        {
            var itemStateChanged = model.ToDos.ItemChanged
                .Where(todoChanged => todoChanged.PropertyName == nameof(TodoViewModel.State))
                .Select(todoChanged => !todoChanged.Sender.State);

            var itemStateChangedOrAddedOrDeleted = Observable.Merge(
                itemStateChanged,
                model.ToDos.ItemsAdded.Where(x => x.State == false).Select(_=> true),
                model.ToDos.ItemsRemoved.Where(x => x.State == false).Select(_=> false)
                );

            _numberOfActiveItems = itemStateChangedOrAddedOrDeleted
               .Select(addItem => addItem ? 1 : -1)
               .Scan(0, (x, y) => x + y)
               .ToProperty(this, x => x.NumberOfActiveItems);

            ShowAll = ReactiveCommand.Create(() => model.UpdateFilter(Enums.EFilter.All));
            ShowActive = ReactiveCommand.Create(() => model.UpdateFilter(Enums.EFilter.Active));
            ShowCompleted = ReactiveCommand.Create(() => model.UpdateFilter(Enums.EFilter.Completed));

            _isAllChecked = model.Filter
                .Select(filter => filter == Enums.EFilter.All)
                .ToProperty(this, x => x.IsAllChecked);

            _isActiveChecked = model.Filter
                .Select(filter => filter == Enums.EFilter.Active)
                .ToProperty(this, x => x.IsActiveChecked);

            _isCompletedChecked = model.Filter
                .Select(filter => filter == Enums.EFilter.Completed)
                .ToProperty(this, x => x.IsCompletedChecked);

            ClearSelected = ReactiveCommand.Create(() => model.ToDos
                .RemoveAll(model.ToDos.Where(todo => todo.State == true).ToList())
            );
        }
    }
}
