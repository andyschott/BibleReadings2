using System;

namespace BibleReadings2.Models
{
    public class TimeZoneViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public TimeZoneViewModel(TimeZoneInfo timeZone)
        {
            Id = timeZone.Id;
            Name = $"{timeZone.Id} ({timeZone.StandardName})";
        }

        public override string ToString() => Name;
    }
}