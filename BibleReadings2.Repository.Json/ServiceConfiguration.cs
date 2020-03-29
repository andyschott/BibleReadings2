using BibleReadings2.Repository;
using BibleReadings2.Repository.Json;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceConfiguration
    {
        public static void AddJsonRepository(this IServiceCollection services, string readerFilePath)
        {
            services.AddSingleton<IReadingsRepository, JsonReadingsRepository>();
            services.AddSingleton<ITranslationsRepository, JsonTranslationsRepository>();

            services.AddSingleton<IReaderRepository, JsonReaderRepository>(sp => new JsonReaderRepository(readerFilePath));
        }
    }
}
