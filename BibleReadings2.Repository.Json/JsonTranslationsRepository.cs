using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace BibleReadings2.Repository.Json
{
    public class JsonTranslationsRepository : ITranslationsRepository
    {
        private static readonly Assembly _assembly = typeof(JsonTranslationsRepository).Assembly;
        private static readonly IDictionary<Languages, string> _languages =
            new Dictionary<Languages, string>
            {
                [Languages.English] = "english",
                [Languages.German] = "german",
            };

        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        public async Task<IEnumerable<Translation>> GetTranslations(Languages language)
        {
            using var stream = GetStream(language);
            var translations = await JsonSerializer.DeserializeAsync<IEnumerable<Translation>>(stream, _options).AsTask();

            return translations ?? Enumerable.Empty<Translation>();
        }

        private static Stream GetStream(Languages language)
        {
            if(!_languages.TryGetValue(language, out var name))
            {
                throw new ArgumentException(nameof(language));
            }

            string streamName = $"BibleReadings2.Repository.Json.data.{name}_translations.json";
            var stream =_assembly.GetManifestResourceStream(streamName);

            if (stream is null)
            {
                throw new Exception($"The manifest resource stream '{streamName}' does not exist");
            }

            return stream;
        }
    }
}