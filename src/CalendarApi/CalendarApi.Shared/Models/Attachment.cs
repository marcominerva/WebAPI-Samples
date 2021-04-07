using System;

namespace CalendarApi.Shared.Models
{
    public class Attachment
    {
        public Guid Id { get; set; }

        public string FileName { get; set; }

        public int Length { get; set; }
    }
}
