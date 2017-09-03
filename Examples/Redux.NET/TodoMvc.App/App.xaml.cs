using System.Collections.Immutable;
using System.Windows;
using Redux;
using PrismRedux.NET.StoreNS.States;

namespace PrismRedux.NET.StoreNS.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IStore<ApplicationState> Store { get; private set; }

        public App()
        {
            Store = new Store<ApplicationState>(Reducers.ReduceApplication, new ApplicationState()
            {
                Filter = TodosFilter.All,
                Todos = ImmutableArray.Create<Todo>()
            });
        }
    }
}
