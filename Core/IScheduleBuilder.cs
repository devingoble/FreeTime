using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FreeTime.Models;

namespace FreeTime.Core
{
    public interface IScheduleBuilder
    {
        Task<List<TimeSlot>> GetAvailableTimeSlots(DateTime targetDate, TimeSlotParameters parameters);
    }
}