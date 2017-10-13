using System.Windows.Controls;

namespace TodoMvc.App
{
    /// <summary>
    /// Interaction logic for Header.xaml
    /// </summary>
    public partial class Header : UserControl
    {
        public Header()
        {
            DataContext = new HeaderViewModel(App.Store);
            InitializeComponent();
        }
    }
}
