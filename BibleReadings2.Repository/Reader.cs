using System;

namespace BibleReadings2.Repository
{
    public class Reader
    {
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; }

        public override string ToString() => $"{Name} @ {Date.ToShortDateString()}";
    }
}