using System;

namespace TodoMvc.States
{
    public class Todo
    {
        public string Title { get; set; }

        public bool IsCompleted { get; set; }

        public Guid Id { get; set; }
    }
}
