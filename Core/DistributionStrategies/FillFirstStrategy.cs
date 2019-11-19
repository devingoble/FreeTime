using FreeTime.Models;
using System.Collections.Generic;
using System.Linq;

namespace FreeTime.Core.DistributionStrategies
{
    public class FillFirstStrategy : ICalendarDistributionStrategy
    {
        public List<CalendarTimeSlot> DistributeAvailableSlots(Dictionary<string, List<AvailableSlot>> availableSlots)
        {
            var scheduledSlots = new List<CalendarTimeSlot>();
            
            var currentCal = CalendarTraversal.GetNextCalendar(availableSlots.Keys.ToList());
            
            foreach (var slot in availableSlots[currentCal])
            {
                if (slot.IsAvailable)
                {
                    scheduledSlots.Add(new CalendarTimeSlot(slot.Start, slot.End, currentCal));
                }
                else
                {
                    var nextSlot = CalendarTraversal.GetNextAvailableSlot(availableSlots, slot, currentCal);

                    if(nextSlot != null)
                    {
                        scheduledSlots.Add(nextSlot);
                    }
                }
            }

            return scheduledSlots;
        }
    }
}
