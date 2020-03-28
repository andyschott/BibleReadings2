using System;
using System.Collections.Generic;
using System.Linq;

namespace BibleReadings2.Models
{
    public class ReadingsViewModel
    {
        public DateTime Date { get; set; }
        public string DateString => Date.ToString("D");

        public IEnumerable<ReadingViewModel> Readings { get; set; } = Enumerable.Empty<ReadingViewModel>();
    }
}