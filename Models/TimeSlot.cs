using System;

namespace FreeTime.Models
{
    public class TimeSlot
    {
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }
        public string Calendar { get; set; }

        public TimeSlot(DateTime start, DateTime end, string calendar)
        {
            Start = start;
            End = end;
            Calendar = calendar.Split("@")[0];
        }
    }
}
