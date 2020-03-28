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
    }
}
