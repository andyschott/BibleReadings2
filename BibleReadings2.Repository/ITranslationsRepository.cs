using System.Collections.Generic;
using System.Threading.Tasks;

namespace BibleReadings2.Repository
{
    public interface ITranslationsRepository
    {
         Task<IEnumerable<Translation>> GetTranslations(Languages language);
    }
}