﻿using System;

namespace FreeTime.Models
{
    public class TimeSlot
    {
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }
        
        public TimeSlot(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public TimeSlot(TimeSlot timeSlot)
        {
            Start = timeSlot.Start;
            End = timeSlot.End;
        }
    }
}
