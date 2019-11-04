using FreeTime.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace FreeTime.Core
{
    public class ScheduleBuilder : IScheduleBuilder
    {
        private readonly ICalendarClientFactory _calendarClientFactory;

        public ScheduleBuilder(ICalendarClientFactory calendarClientFactory)
        {
            _calendarClientFactory = calendarClientFactory;
        }

        public async Task<List<TimeSlot>> GetAvailableTimeSlots(DateTime targetDate, TimeSlotParameters parameters)
        {
            var (Open, Close) = BuildDateRange(targetDate, parameters.Open, parameters.Close);
            var calClient = _calendarClientFactory.GetCalendarClient(parameters.Calendars);
            var allSlots = calClient.GetAllTimeSlots(Open, Close, parameters.DurationMinutes);
            var fullSlots = await calClient.GetFullTimeSlots(Open, Close);
            
            foreach (var slot in fullSlots)
            {
                allSlots.RemoveAll(s => s.Start >= slot.Start && s.End <= slot.End && s.Calendar == slot.Calendar);
            }

            var availableSlots = new List<TimeSlot>();

            foreach (var slot in allSlots)
            {
                if(!availableSlots.Any(s => s.Start == slot.Start && s.End == slot.End))
                {
                    availableSlots.Add(slot);
                }
            }

            return availableSlots;
        }

        private (DateTime Open, DateTime Close) BuildDateRange(DateTime targetDate, DateTime open, DateTime close)
        {
            var openDT = new DateTime(targetDate.Year, targetDate.Month, targetDate.Day, open.Hour, open.Minute, 0);
            var closeDT = new DateTime(targetDate.Year, targetDate.Month, targetDate.Day, close.Hour, close.Minute, 0);

            return (openDT, closeDT);
        }
    }
}
