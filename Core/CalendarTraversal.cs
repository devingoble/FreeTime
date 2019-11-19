using FreeTime.Models;
using System.Collections.Generic;
using System.Linq;

namespace FreeTime.Core
{
    public static class CalendarTraversal
    {
        public static string GetNextCalendar(List<string> allCals, string currentCal = "")
        {
            if (!allCals.Contains(currentCal))
            {
                return allCals.First();
            }
            else
            {
                var index = allCals.IndexOf(currentCal);
                if (index < allCals.Count - 1)
                {
                    return allCals[index + 1];
                }
                else
                {
                    return allCals.First();
                }
            }
        }

        public static CalendarTimeSlot? GetNextAvailableSlot(Dictionary<string, List<AvailableSlot>> availableSlots, AvailableSlot currentSlot, string currentCalendar, int depth = 0)
        {
            if (depth >= availableSlots.Keys.Count)
            {
                return null;
            }

            var currentCal = CalendarTraversal.GetNextCalendar(availableSlots.Keys.ToList(), currentCalendar);

            var nextSlot = availableSlots[currentCal].FirstOrDefault(s => s.IsAvailable == true && s.Start == currentSlot.Start && s.End == currentSlot.End);

            if (nextSlot == null)
            {
                return GetNextAvailableSlot(availableSlots, currentSlot, currentCal, depth + 1);
            }
            else
            {
                return new CalendarTimeSlot(nextSlot.Start, nextSlot.End, currentCal);
            }
        }

    }
}
