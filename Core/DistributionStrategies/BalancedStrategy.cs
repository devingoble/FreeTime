using FreeTime.Models;
using System.Collections.Generic;
using System.Linq;

namespace FreeTime.Core.DistributionStrategies
{
    public class BalancedStrategy : ICalendarDistributionStrategy
    {
        public List<CalendarTimeSlot> DistributeAvailableSlots(Dictionary<string, List<AvailableSlot>> availableSlots)
        {
            var scheduledSlots = new List<CalendarTimeSlot>();

            var currentCal = CalendarTraversal.GetNextCalendar(availableSlots.Keys.ToList());
            var slotCount = availableSlots[currentCal].Count;

            for (int i = 0; i < slotCount; i++)
            {
                var currentSlot = availableSlots[currentCal][i];

                if (currentSlot.IsAvailable)
                {
                    scheduledSlots.Add(new CalendarTimeSlot(currentSlot.Start, currentSlot.End, currentCal));
                }
                else
                {
                    var nextSlot = CalendarTraversal.GetNextAvailableSlot(availableSlots, currentSlot, currentCal);

                    if(nextSlot != null)
                    {
                        scheduledSlots.Add(nextSlot);
                    }
                }

                currentCal = CalendarTraversal.GetNextCalendar(availableSlots.Keys.ToList(), currentCal);
            }

            return scheduledSlots;
        }
    }
}
