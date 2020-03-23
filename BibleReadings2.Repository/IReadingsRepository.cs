using System;

namespace BibleReadings2.Repository
{
    public interface IReadingsRepository
    {
        Day GetReadings(int month, int year);
    }
}
