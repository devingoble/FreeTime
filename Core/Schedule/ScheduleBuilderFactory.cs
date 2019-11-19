using FreeTime.Core.Calendar;
using FreeTime.Core.DistributionStrategies;

namespace FreeTime.Core.Schedule
{
    public class ScheduleBuilderFactory : IScheduleBuilderFactory
    {
        private readonly ICalendarClientFactory _calendarClientFactory;

        public ScheduleBuilderFactory(ICalendarClientFactory calendarClientFactory)
        {
            _calendarClientFactory = calendarClientFactory;
        }

        public ScheduleBuilder GetScheduleBuilder(string distributionStrategy)
        {
            var sb = distributionStrategy switch
            {
                "fill" => new ScheduleBuilder(_calendarClientFactory, new FillFirstStrategy()),
                "balanced" => new ScheduleBuilder(_calendarClientFactory, new BalancedStrategy()),
                _ => new ScheduleBuilder(_calendarClientFactory, new FillFirstStrategy())
            };

            return sb;
        }
    }
}
