using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FreeTime.Models;

namespace FreeTime.Core
{
    public interface ICalendarClient
    {
        Task<List<TimeSlot>> GetFullTimeSlots(DateTime open, DateTime close);
    }
}