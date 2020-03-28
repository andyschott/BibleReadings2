using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BibleReadings2.Repository.Json.Test
{
    public class RepositoryTests
    {
        private readonly JsonReadingsRepository _repository = new JsonReadingsRepository();

        [Fact]
        public async Task LoadReadingSuccessfully()
        {
            var day = await _repository.GetReadings(1, 1);
            Assert.Equal(2, day.Readings.Count());
        }

        [Fact]
        public Task LoadInvalidDay()
        {
            return Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _repository.GetReadings(2, 30));
        }

        [Fact]
        public Task LoadInvalidMonth()
        {
            return Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _repository.GetReadings(13, 1));
        }
    }
}
