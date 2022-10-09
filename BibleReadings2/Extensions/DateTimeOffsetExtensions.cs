using System;
using System.Linq;

namespace BibleReadings2.Extensions
{
    public static class DateTimeOffsetExtensions
    {
        public static DateTimeOffset ToTimeZone(this DateTimeOffset date, string? timeZoneId)
        {
            if (string.IsNullOrWhiteSpace(timeZoneId))
            {
                return date;
            }

            var timeZone = GetTimeZone(timeZoneId);
            return date.ToOffset(timeZone.BaseUtcOffset);
        }

        private static TimeZoneInfo GetTimeZone(string id)
        {
            var timeZones = TimeZoneInfo.GetSystemTimeZones();
            var timeZone = timeZones.FirstOrDefault(timeZone => timeZone.Id.Equals(id));
            if(timeZone is null)
            {
                return TimeZoneInfo.Utc;
            }

            return timeZone;
        }
    }
}