using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace ReactiveUIExample
{
    public class HeaderViewModel : ReactiveObject
    {
        public HeaderViewModel(IModel model)
        {
            Add = ReactiveCommand.Create(() => {
                model.ToDos.Add(new TodoViewModel() { State = false, Text = Text }); 
                });
            OnAll = ReactiveCommand.Create(() => {
                bool bAllCompleted = model.ToDos.All(todo => todo.State == true);
                foreach (var todo in model.ToDos)
                {
                    todo.State = !bAllCompleted;
                }
            });
 
        }

        private string _text;
        public string Text
        {
            get { return _text; }
            set { this.RaiseAndSetIfChanged(ref _text, value); }
        }

        public ReactiveCommand Add { get; private set; }
        public ReactiveCommand OnAll { get; private set; }
    }
}
