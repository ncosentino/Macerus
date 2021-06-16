using System.IO;
using System.Threading.Tasks;

namespace Macerus.Plugins.Features.Mapping
{
    public interface IMapResourceLoader
    {
        Task<Stream> LoadStreamAsync(string pathToResource);
    }
}