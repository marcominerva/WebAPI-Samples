using System;
using System.Collections.Generic;

namespace CalendarApi.DataAccessLayer.Entities
{
    public class Event
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Attachment> Attachments { get; set; }
    }
}
