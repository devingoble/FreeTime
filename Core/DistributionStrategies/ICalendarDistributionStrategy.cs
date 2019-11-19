using FreeTime.Models;
using System.Collections.Generic;

namespace FreeTime.Core.DistributionStrategies
{
    public interface ICalendarDistributionStrategy
    {
        List<CalendarTimeSlot> DistributeAvailableSlots(Dictionary<string, List<AvailableSlot>> availableSlots);
    }
}
