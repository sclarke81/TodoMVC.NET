using System;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TodoMvc.App
{
    /// <summary>
    /// Interaction logic for Header.xaml
    /// </summary>
    public partial class Header : UserControl
    {
        public Header()
        {
            InitializeComponent();

            App.Store
                .Select(Selectors.MakeHeaderViewModel)
                .DistinctUntilChanged(model => model.MarkAllAsCompleteIsChecked)
                .Subscribe(model => markAllAsComplete.IsChecked = model.MarkAllAsCompleteIsChecked);

            App.Store
                .Select(Selectors.MakeHeaderViewModel)
                .DistinctUntilChanged(model => model.MarkAllAsCompleteIsVisible)
                .Subscribe(model => markAllAsComplete.Visibility = model.MarkAllAsCompleteIsVisible? Visibility.Visible : Visibility.Hidden);
        }

        private void markAllAsComplete_Click(object sender, RoutedEventArgs e)
        {
            App.Store.Dispatch(new AllTodosIsCompletedChangedAction() { IsCompleted = markAllAsComplete.IsChecked.Value });
        }

        private void title_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                App.Store.Dispatch(new TodoAddedAction() { Text = title.Text });
                title.Text = "";
                e.Handled = true;
            }
        }
    }
}
