using TodoMvc.States;

namespace TodoMvc.ViewModels
{
    public class FooterViewModel
    {
        public bool ClearCompletedIsVisible { get; set; }

        public string CounterText { get; set; }

        public TodosFilter SelectedFilter { get; set; }
    }
}
