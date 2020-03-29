using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace BibleReadings2.Repository.Json
{
    public class JsonReaderRepository : IReaderRepository
    {
        private readonly string _filePath;
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        public JsonReaderRepository(string filePath)
        {
            _filePath = filePath;
        }

        public async Task<Reader?> GetReader()
        {
            if(!File.Exists(_filePath))
            {
                return null;
            }

            using var stream = new FileStream(_filePath, FileMode.Open);
            var reader = await JsonSerializer.DeserializeAsync<Reader>(stream, _options);

            return reader;
        }

        public Task SaveReader(Reader reader)
        {
            var dir = Path.GetDirectoryName(_filePath);
            if(!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            
            using var stream = new FileStream(_filePath, FileMode.Create);
            return JsonSerializer.SerializeAsync(stream, reader, _options);
        }
    }
}