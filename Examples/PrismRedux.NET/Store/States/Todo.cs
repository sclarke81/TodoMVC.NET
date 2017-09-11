using System;

namespace PrismRedux.NET.StoreNS.States
{
    public class Todo
    {
        public string Title { get; set; }

        public bool IsCompleted { get; set; }

        public Guid Id { get; set; }
    }
}
