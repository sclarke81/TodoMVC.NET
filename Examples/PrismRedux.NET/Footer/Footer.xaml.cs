using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PrismRedux.NET.StoreNS;
using PrismRedux.NET.StoreNS.States;
using Redux;

namespace Footer
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    [Export]
    public partial class Footer : UserControl
    {
        private IStore<ApplicationState> _store;

        [ImportingConstructor]
        public Footer(IStore<ApplicationState> store)
        {
            InitializeComponent();
            _store = store;
            _store
                .Select(Selectors.MakeFooterViewModel)
                .DistinctUntilChanged(model => model.ClearCompletedIsVisible)
                .Subscribe(model => clearCompleted.Visibility = model.ClearCompletedIsVisible ? Visibility.Visible : Visibility.Hidden);

            _store
                .Select(Selectors.MakeFooterViewModel)
                .DistinctUntilChanged(model => model.CounterText)
                .Subscribe(model => counter.Content = model.CounterText);

            _store
                .Select(Selectors.MakeFooterViewModel)
                .DistinctUntilChanged(model => model.SelectedFilter)
                .Subscribe(model => UpdateRadioButtons(model.SelectedFilter));
        }

        private void UpdateRadioButtons(TodosFilter filter)
        {
            switch (filter)
            {
                case TodosFilter.Active:
                {
                    filterActive.IsChecked = true;
                    break;
                }
                case TodosFilter.Completed:
                {
                    filterCompleted.IsChecked = true;
                    break;
                }
                case TodosFilter.All:
                default:
                {
                    filterAll.IsChecked = true;
                    break;
                }
            }
        }

        private void clearCompleted_Click(object sender, RoutedEventArgs e)
        {
            _store.Dispatch(new CompletedTodosClearedAction());
        }

        private void filterAll_Click(object sender, RoutedEventArgs e)
        {
            _store.Dispatch(new FilterChangedAction() { Filter = TodosFilter.All });
        }

        private void filterActive_Click(object sender, RoutedEventArgs e)
        {
            _store.Dispatch(new FilterChangedAction() { Filter = TodosFilter.Active });
        }

        private void filterCompleted_Click(object sender, RoutedEventArgs e)
        {
            _store.Dispatch(new FilterChangedAction() { Filter = TodosFilter.Completed });
        }
    }
}

