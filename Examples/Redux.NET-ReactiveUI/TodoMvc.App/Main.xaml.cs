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
            DataContext = new MainViewModel(App.Store);
            InitializeComponent();
        }
    }
}
