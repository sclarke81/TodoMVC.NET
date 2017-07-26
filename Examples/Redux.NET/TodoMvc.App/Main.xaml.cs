using System;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TodoMvc.App
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : UserControl
    {
        public Main()
        {
            InitializeComponent();

            App.Store
                .Select(Selectors.MakeMainViewModel)
                .DistinctUntilChanged(model => model.Todos)
                .Subscribe(model => todos.ItemsSource = model.Todos);
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            var id = (Guid)checkBox.Tag;
            App.Store.Dispatch(new TodoIsCompletedChangedAction() { Id = id, IsCompleted = checkBox.IsChecked.Value });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var id = (Guid)button.Tag;
            App.Store.Dispatch(new TodoDeletedAction() { Id = id });
        }
    }
}
