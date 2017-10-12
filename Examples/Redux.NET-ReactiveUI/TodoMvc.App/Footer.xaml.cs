using System;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using TodoMvc.States;

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
