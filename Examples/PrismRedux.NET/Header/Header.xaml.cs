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

namespace PrismRedux.NET.Header
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    [Export]
    public partial class Header : UserControl
    {
        private IStore<ApplicationState> _store;

        [ImportingConstructor]
        public Header(IStore<ApplicationState> store)
        {
            InitializeComponent();
            _store = store;
            _store
                .Select(Selectors.MakeHeaderViewModel)
                .DistinctUntilChanged(model => model.MarkAllAsCompleteIsChecked)
                .Subscribe(model => markAllAsComplete.IsChecked = model.MarkAllAsCompleteIsChecked);

            _store
                .Select(Selectors.MakeHeaderViewModel)
                .DistinctUntilChanged(model => model.MarkAllAsCompleteIsVisible)
                .Subscribe(model => markAllAsComplete.Visibility = model.MarkAllAsCompleteIsVisible ? Visibility.Visible : Visibility.Hidden);
        }

        private void markAllAsComplete_Click(object sender, RoutedEventArgs e)
        {
            _store.Dispatch(new AllTodosIsCompletedChangedAction() { IsCompleted = markAllAsComplete.IsChecked.Value });
        }

        private void title_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                _store.Dispatch(new TodoAddedAction() { Text = title.Text });
                title.Text = "";
                e.Handled = true;
            }
        }
    }
}
