using System.Threading.Tasks;

namespace BibleReadings2.Repository
{
    public interface IReaderRepository
    {
         Task<Reader?> GetReader();
         Task SaveReader(Reader reader);
    }
}