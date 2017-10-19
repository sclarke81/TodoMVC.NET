using System.Linq;
using System.Reactive.Linq;
using System.Windows;
using ReactiveUI;
using Redux;
using TodoMvc.States;

namespace TodoMvc.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        private readonly ObservableAsPropertyHelper<Visibility> _mainAndFooterVisibility;
        public Visibility MainAndFooterVisibility => _mainAndFooterVisibility.Value;

        public MainWindowViewModel(IStore<ApplicationState> store)
        {
            store.Select(s => s.Todos.Any() ? Visibility.Visible : Visibility.Hidden)
                 .DistinctUntilChanged()
                 .ToProperty(this, x => x.MainAndFooterVisibility, out _mainAndFooterVisibility);
        }
    }
}
