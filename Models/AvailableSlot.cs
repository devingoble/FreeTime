using System;

namespace FreeTime.Models
{
    public class AvailableSlot
    {
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }
        public bool IsAvailable { get; set; }

        public AvailableSlot(DateTime start, DateTime end, bool isAvailable)
        {
            Start = start;
            End = end;
            IsAvailable = isAvailable;
        }
    }
}
