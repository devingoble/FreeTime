using System;
using System.Collections.Generic;

namespace FreeTime.Models
{
    public class ScheduleSlot
    {
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }
        public List<string> Calendars { get; set; } = new List<string>();

        public ScheduleSlot(DateTime start, DateTime end, List<string> calendars)
        {
            Start = start;
            End = end;
            Calendars = calendars;
        }

    }
}
