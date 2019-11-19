namespace FreeTime.Core.Schedule
{
    public interface IScheduleBuilderFactory
    {
        ScheduleBuilder GetScheduleBuilder(string distributionStrategy);
    }
}
