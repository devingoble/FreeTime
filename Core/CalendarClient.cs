using FreeTime.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace FreeTime.Core
{
    public class CalendarClient : ICalendarClient
    {
        private readonly HttpClient _httpClient;
        private readonly ExchangeOptions _exchangeOptions;
        private readonly List<string> _calendars;

        public CalendarClient(HttpClient httpClient, ExchangeOptions exchangeOptions, List<string> calendars)
        {
            _httpClient = httpClient;
            _exchangeOptions = exchangeOptions;
            _calendars = calendars;
        }

        public async Task<List<TimeSlot>> GetFullTimeSlots(DateTime open, DateTime close)
        {
            var fullSlots = new List<TimeSlot>();

            foreach (var cal in _calendars)
            {
                var uri = ExchangeURIBuilder.GetUri(_exchangeOptions, cal + _exchangeOptions.CalendarSuffix, open, close);
                using var request = new HttpRequestMessage(HttpMethod.Get, uri);
                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var odataResponse = JsonSerializer.Deserialize<ODataResponse>(await response.Content.ReadAsStringAsync());
                    fullSlots.AddRange(odataResponse.Value.Select(v => new TimeSlot(v.Start.DateTime, v.End.DateTime, cal)));
                }
            }

            return fullSlots;
        }

        public List<TimeSlot> GetAllTimeSlots(DateTime open, DateTime close, int duration)
        {
            var allSlots = new List<TimeSlot>();
            var currentTime = open;

            while (currentTime < close)
            {
                var endTime = currentTime.AddMinutes(duration);

                foreach (var cal in _calendars)
                {
                    allSlots.Add(new TimeSlot(currentTime, endTime, cal));
                }

                currentTime = endTime;
            }

            return allSlots;
        }
    }
}
