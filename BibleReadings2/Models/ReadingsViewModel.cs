using System;
using System.Collections.Generic;
using System.Linq;
using BibleReadings2.Helpers;

namespace BibleReadings2.Models
{
    public class ReadingsViewModel
    {
        public DateTime Date { get; set; }
        public string DateString => Date.ToString("D");

        public DateTime Next => Date.AddDays(1);
        public DateTime Previous => Date.AddDays(-1);

        public string English { get; set; } = string.Empty;
        public string German { get; set; } = string.Empty;

        public IEnumerable<ReadingViewModel> Readings { get; set; } = Enumerable.Empty<ReadingViewModel>();

        public string BuildAllReadingsLink() => BibleGateway.BuildAllReadingsUrl(Readings.Select(reading => reading.Reading), English, German);
    }
}