using System;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using PrismRedux.NET.StoreNS.States;

namespace PrismRedux.NET.StoreNS.App
{
    /// <summary>
    /// Interaction logic for Footer.xaml
    /// </summary>
    public partial class Footer : UserControl
    {
        public Footer()
        {
            InitializeComponent();

            App.Store
                .Select(Selectors.MakeFooterViewModel)
                .DistinctUntilChanged(model => model.ClearCompletedIsVisible)
                .Subscribe(model => clearCompleted.Visibility = model.ClearCompletedIsVisible ? Visibility.Visible : Visibility.Hidden);

            App.Store
                .Select(Selectors.MakeFooterViewModel)
                .DistinctUntilChanged(model => model.CounterText)
                .Subscribe(model => counter.Content = model.CounterText);

            App.Store
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
            App.Store.Dispatch(new CompletedTodosClearedAction());
        }

        private void filterAll_Click(object sender, RoutedEventArgs e)
        {
            App.Store.Dispatch(new FilterChangedAction() { Filter = TodosFilter.All });
        }

        private void filterActive_Click(object sender, RoutedEventArgs e)
        {
            App.Store.Dispatch(new FilterChangedAction() { Filter = TodosFilter.Active });
        }

        private void filterCompleted_Click(object sender, RoutedEventArgs e)
        {
            App.Store.Dispatch(new FilterChangedAction() { Filter = TodosFilter.Completed });
        }
    }
}
