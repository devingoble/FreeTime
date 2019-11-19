using FreeTime.Models;
using System;
using System.Text;

namespace FreeTime.Core
{
    public static class ExchangeURIBuilder
    {
        public static Uri GetUri(ExchangeOptions exchangeOptions, string calendar, DateTime start, DateTime end)
        {
            var builder = new UriBuilder();
            builder.Scheme = exchangeOptions.Protocol;
            builder.Host = exchangeOptions.Server;
            builder.Path = $"{exchangeOptions.APIPath}/{calendar}/{exchangeOptions.Resource}";

            var queryBuilder = new StringBuilder();
            queryBuilder.Append("startDateTime=");
            queryBuilder.Append(start.ToString("yyyy-MM-dd") + "T" + start.ToString("HH:mm:ss"));
            queryBuilder.Append("&");
            queryBuilder.Append("endDateTime=");
            queryBuilder.Append(end.ToString("yyyy-MM-dd") + "T" + end.ToString("HH:mm:ss"));
            queryBuilder.Append("&");
            queryBuilder.Append("$select=Start,End");
            queryBuilder.Append("&");
            queryBuilder.Append("format=JSON");

            builder.Query = queryBuilder.ToString();

            return builder.Uri;
        }
    }
}
