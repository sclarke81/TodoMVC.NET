using System.Windows.Controls;
using TodoMvc.ViewModels;

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
