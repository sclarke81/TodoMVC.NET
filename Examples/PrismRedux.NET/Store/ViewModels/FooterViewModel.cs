using PrismRedux.NET.StoreNS.States;

namespace PrismRedux.NET.StoreNS.ViewModels
{
    public class FooterViewModel
    {
        public bool ClearCompletedIsVisible { get; set; }

        public string CounterText { get; set; }

        public TodosFilter SelectedFilter { get; set; }
    }
}
