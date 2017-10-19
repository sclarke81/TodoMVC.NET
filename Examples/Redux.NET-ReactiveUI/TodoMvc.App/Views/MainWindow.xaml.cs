using System.Windows;
using TodoMvc.ViewModels;

namespace TodoMvc.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new MainWindowViewModel(App.Store);
            InitializeComponent();
        }
    }
}
