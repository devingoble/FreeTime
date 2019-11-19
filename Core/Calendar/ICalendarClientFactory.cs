using System.Collections.Generic;

namespace FreeTime.Core.Calendar
{
    public interface ICalendarClientFactory
    {
        ICalendarClient GetCalendarClient(List<string> calendars);
    }
}