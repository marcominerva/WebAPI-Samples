using System;

namespace CalendarApi.DataAccessLayer.Entities
{
    public class Attachment
    {
        public Guid Id { get; set; }

        public Guid EventId { get; set; }

        public string Path { get; set; }

        public int Length { get; set; }

        public virtual Event Event { get; set; }
    }
}
