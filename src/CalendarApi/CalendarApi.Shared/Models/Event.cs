using System;

namespace CalendarApi.Shared.Models
{
    public class Event
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }
    }
}
