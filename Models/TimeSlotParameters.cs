using System;
using System.Collections.Generic;

namespace FreeTime.Models
{
    public class TimeSlotParameters
    {
        public DateTime Open { get; set; }
        public DateTime Close { get; set; }
        public int DurationMinutes { get; set; }
        public List<string> Calendars { get; set; } = new List<string>();
    }
}
