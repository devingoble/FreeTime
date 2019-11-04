using System.Collections.Generic;

namespace FreeTime.Core
{
    public interface ICalendarClientFactory
    {
        CalendarClient GetCalendarClient(List<string> calendars);
    }
}