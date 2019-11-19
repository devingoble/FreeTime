using System;

namespace FreeTime.Models
{
    public class CalendarTimeSlot
    {
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }
        public string Calendar { get; set; }

        public CalendarTimeSlot(DateTime start, DateTime end, string calendar)
        {
            Start = start;
            End = end;
            Calendar = calendar;
        }
    }
}
