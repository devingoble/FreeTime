using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FreeTime.Models;

namespace FreeTime.Core.Calendar
{
    public interface ICalendarClient
    {
        Task<Dictionary<string, List<TimeSlot>>> GetFullTimeSlots(DateTime open, DateTime close);
        Dictionary<string, List<TimeSlot>> GetAllTimeSlots(DateTime open, DateTime close, int duration);
    }
}