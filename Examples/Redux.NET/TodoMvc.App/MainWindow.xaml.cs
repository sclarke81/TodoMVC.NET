using System;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;

namespace PrismRedux.NET.StoreNS.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            App.Store
                .Select(Selectors.MakeApplicationViewModel)
                .DistinctUntilChanged(model => model.MainAndFooterAreVisible)
                .Subscribe(model => SetMainAndFooterVisibility(model.MainAndFooterAreVisible));
        }

        private void SetMainAndFooterVisibility(bool isVisible)
        {
            Visibility visibility = isVisible ? Visibility.Visible : Visibility.Collapsed;
            main.Visibility = visibility;
            footer.Visibility = visibility;
        }
    }
}
