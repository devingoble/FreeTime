using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FreeTime.Models;

namespace FreeTime.Core.Schedule
{
    public interface IScheduleBuilder
    {
        Task<List<CalendarTimeSlot>> GetAvailableTimeSlots(DateTime targetDate, TimeSlotParameters parameters);
    }
}