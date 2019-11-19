using FreeTime.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using FreeTime.Core.DistributionStrategies;
using FreeTime.Core.Calendar;

namespace FreeTime.Core.Schedule
{
    public class ScheduleBuilder : IScheduleBuilder
    {
        private readonly ICalendarClientFactory _calendarClientFactory;
        private readonly ICalendarDistributionStrategy _distributionStrategy;

        public ScheduleBuilder(ICalendarClientFactory calendarClientFactory, ICalendarDistributionStrategy distributionStrategy)
        {
            _calendarClientFactory = calendarClientFactory;
            _distributionStrategy = distributionStrategy;
        }

        public async Task<List<CalendarTimeSlot>> GetAvailableTimeSlots(DateTime targetDate, TimeSlotParameters parameters)
        {
            var (Open, Close) = BuildDateRange(targetDate, parameters.Open, parameters.Close);
            var calClient = _calendarClientFactory.GetCalendarClient(parameters.Calendars);
            var allSlots = calClient.GetAllTimeSlots(Open, Close, parameters.DurationMinutes);
            var fullSlots = await calClient.GetFullTimeSlots(Open, Close);
            var availableSlots = BuildAvailableSlots(allSlots, fullSlots);

            return _distributionStrategy.DistributeAvailableSlots(availableSlots);
        }

        private static Dictionary<string, List<AvailableSlot>> BuildAvailableSlots(Dictionary<string, List<TimeSlot>> allSlots, Dictionary<string, List<TimeSlot>> fullSlots)
        {
            var availableSlots = new Dictionary<string, List<AvailableSlot>>();
            var cals = allSlots.Keys.ToList();

            foreach (var cal in cals)
            {
                var currentCalSlots = new List<AvailableSlot>();
                
                foreach (var slot in allSlots[cal])
                {
                    var isAvailable = !fullSlots[cal].Contains(slot, new TimeSlotOverlapComparer());
                    currentCalSlots.Add(new AvailableSlot(slot.Start, slot.End, isAvailable));
                }

                availableSlots.Add(cal, currentCalSlots);
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
