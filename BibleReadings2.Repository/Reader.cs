using System;

namespace BibleReadings2.Repository
{
    public class Reader
    {
        public string Name { get; set; } = string.Empty;

        private DateTime _date;
        public DateTime Date
        {
            get { return _date.ToLocalTime().Date; }
            set { _date = value; }
        }

        public override string ToString() => $"{Name} @ {Date.ToShortDateString()}";
    }
}