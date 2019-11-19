using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FreeTime.Core.Schedule;
using FreeTime.Models;
using Microsoft.AspNetCore.Mvc;

namespace FreeTime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly IScheduleBuilderFactory _scheduleBuilderFactory;

        public CalendarController(IScheduleBuilderFactory scheduleBuilderFactory)
        {
            _scheduleBuilderFactory = scheduleBuilderFactory;
        }

        // GET: api/Calendar
        [HttpGet("{targetDate:datetime}")]
        public async Task<ActionResult<List<CalendarTimeSlot>>> Get(DateTime targetDate, [FromQuery] TimeSlotParameters parameters, [FromQuery] string distributionStrategy)
        {
            var sbFactory = _scheduleBuilderFactory.GetScheduleBuilder(distributionStrategy);
            var timeSlots = await sbFactory.GetAvailableTimeSlots(targetDate, parameters);

            return Ok(timeSlots);
        }
    }
}
