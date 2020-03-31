using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BibleReadings2.Helpers;
using BibleReadings2.Repository;

namespace BibleReadings2.Models
{
    public class ReadingsViewModel
    {
        private static readonly DateTimeFormatInfo _dateFormat = new CultureInfo("en-US").DateTimeFormat;
        
        public DateTime Date { get; set; }
        public string DateString => Date.ToString(_dateFormat.LongDatePattern);

        public DateTime Next => Date.AddDays(1);
        public DateTime Previous => Date.AddDays(-1);

        public string English { get; set; } = string.Empty;
        public string German { get; set; } = string.Empty;

        public Reader? LastReader { get; set; }

        public IEnumerable<ReadingViewModel> Readings { get; set; } = Enumerable.Empty<ReadingViewModel>();

        public string BuildAllReadingsLink() => BibleGateway.BuildAllReadingsUrl(Readings.Select(reading => reading.Reading), English, German);
    }
}