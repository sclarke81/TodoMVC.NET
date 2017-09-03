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

namespace Main
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    [Export]
    public partial class Main : UserControl
    {
        private IStore<ApplicationState> _store;

        [ImportingConstructor]
        public Main(IStore<ApplicationState> store)
        {
            InitializeComponent();
            _store = store;
            _store
                .Select(Selectors.MakeMainViewModel)
                .DistinctUntilChanged(model => model.Todos)
                .Subscribe(model => todos.ItemsSource = model.Todos);
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            var id = (Guid)checkBox.Tag;
            _store.Dispatch(new TodoIsCompletedChangedAction() { Id = id, IsCompleted = checkBox.IsChecked.Value });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var id = (Guid)button.Tag;
            _store.Dispatch(new TodoDeletedAction() { Id = id });
        }
    }
}
