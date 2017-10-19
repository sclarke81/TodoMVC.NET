using System.Windows.Controls;
using TodoMvc.ViewModels;

namespace TodoMvc.App
{
    /// <summary>
    /// Interaction logic for Footer.xaml
    /// </summary>
    public partial class Footer : UserControl
    {
        public Footer()
        {
            DataContext = new FooterViewModel(App.Store);
            InitializeComponent();
        }
    }
}
