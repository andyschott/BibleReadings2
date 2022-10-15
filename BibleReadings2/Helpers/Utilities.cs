using System;
using System.Linq;
using BibleReadings2.Extensions;
using BibleReadings2.Repository;

namespace BibleReadings2.Helpers
{
    public static class Utilities
    {
        public static string BuildReaderDescription(Reader? reader)
        {
            if(reader == null)
            {
                return "No one has read the devotion before.";
            }

            return $"{reader.Name} read the devotion {DateDescription(reader.Date)}.";
        }

        private static string DateDescription(DateTimeOffset date)
        {
            if(date == default)
            {
                return "last time";
            }

            var today = new DateTimeOffset(DateTime.UtcNow, TimeSpan.Zero)
                .ToOffset(date.Offset);
            if(date.IsSameDay(today))
            {
                return "today";
            }

            var yesterday = today.AddDays(-1);
            if(date.IsSameDay(yesterday))
            {
                return "yesterday";
            }

            var lastWeek = today.AddDays(-7);
            if(date >= lastWeek)
            {
                return date.DayOfWeek.ToString();
            }

            return date.ToString("D");
        }

        private static bool IsSameDay(this DateTimeOffset date, DateTimeOffset other)
        {
            return date.Year == other.Year &&
                date.Month == other.Month &&
                date.Day == other.Day;
        }
    }
}