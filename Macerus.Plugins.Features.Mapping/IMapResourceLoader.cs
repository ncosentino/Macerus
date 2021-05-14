using System.IO;

namespace Macerus.Plugins.Features.Mapping
{
    public interface IMapResourceLoader
    {
        Stream LoadStream(string pathToResource);
    }
}