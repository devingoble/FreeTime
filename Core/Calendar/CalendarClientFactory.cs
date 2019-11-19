using FreeTime.Models;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;

namespace FreeTime.Core.Calendar
{
    public class CalendarClientFactory : ICalendarClientFactory
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ExchangeOptions _exchangeOptions;

        public CalendarClientFactory(IHttpClientFactory httpClientFactory, IOptionsMonitor<ExchangeOptions> exchangeOptions)
        {
            _httpClientFactory = httpClientFactory;
            _exchangeOptions = exchangeOptions.CurrentValue;
        }

        public ICalendarClient GetCalendarClient(List<string> calendars)
        {
            var client = new CalendarClient(_httpClientFactory.CreateClient("exchange"), _exchangeOptions, calendars);

            return client;
        }
    }
}
