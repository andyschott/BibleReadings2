using System;
using System.Threading.Tasks;

namespace BibleReadings2.Repository
{
    public interface IReadingsRepository
    {
        Task<Day> GetReadings(int month, int day);
    }
}
