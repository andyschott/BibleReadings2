using System;

namespace BibleReadings2.Repository
{
    public class Reader
    {
        public string Name { get; set; } = string.Empty;

        public DateTimeOffset Date { get; set; }

        public override string ToString() => $"{Name} @ {Date.Date.ToShortDateString()}";
    }
}