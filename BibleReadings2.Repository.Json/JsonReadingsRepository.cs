using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace BibleReadings2.Repository.Json
{
    public class JsonReadingsRepository : IReadingsRepository
    {
        private static readonly Assembly _assembly = typeof(JsonReadingsRepository).Assembly;
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        public async Task<Day> GetReadings(int month, int day)
        {
            using var stream = GetStream(month);
            var days = await JsonSerializer.DeserializeAsync<Day[]>(stream, _options);
            return days[day - 1];
        }

        private static Stream GetStream(int month)
        {
            return _assembly.GetManifestResourceStream($"BibleReadings2.Repository.Json.data.{month}.json");
        }
    }
}
