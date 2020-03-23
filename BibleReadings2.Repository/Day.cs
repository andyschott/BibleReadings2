using System;
using System.Collections.Generic;

namespace BibleReadings2.Repository
{
    public class Day
    {
        public IEnumerable<Reading> Readings { get; set; } = new List<Reading>();
    }
}