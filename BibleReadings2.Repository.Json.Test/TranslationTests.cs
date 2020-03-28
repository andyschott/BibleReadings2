using System;
using System.Threading.Tasks;
using Xunit;

namespace BibleReadings2.Repository.Json.Test
{
    public class TranslationTests
    {
        private readonly JsonTranslationsRepository _repository = new JsonTranslationsRepository();

        [Fact]
        public async Task LoadTranslationsSuccessfully()
        {
            var translations = await _repository.GetTranslations(Languages.English);
            Assert.NotEmpty(translations);
        }

        [Fact]
        public Task LoadInvalidTranslations()
        {
            return Assert.ThrowsAsync<ArgumentException>(() => _repository.GetTranslations(Languages.Unknown));
        }
    }
}