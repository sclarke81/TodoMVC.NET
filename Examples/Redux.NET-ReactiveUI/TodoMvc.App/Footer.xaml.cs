using System.Windows.Controls;

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
