using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FreeTime.Core;
using FreeTime.Models;
using Microsoft.AspNetCore.Mvc;

namespace FreeTime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly IScheduleBuilder _scheduleBuilder;

        public CalendarController(IScheduleBuilder scheduleBuilder)
        {
            _scheduleBuilder = scheduleBuilder;
        }

        // GET: api/Calendar
        [HttpGet("{targetDate:datetime}")]
        public async Task<ActionResult<List<TimeSlot>>> Get(DateTime targetDate, [FromQuery] TimeSlotParameters parameters)
        {
            var timeSlots = await _scheduleBuilder.GetAvailableTimeSlots(targetDate, parameters);

            return Ok(timeSlots);
        }
    }
}
