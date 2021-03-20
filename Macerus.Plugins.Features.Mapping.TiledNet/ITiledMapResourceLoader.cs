using System.IO;

namespace Macerus.Plugins.Features.Mapping.TiledNet
{
    public interface ITiledMapResourceLoader
    {
        Stream LoadStream(string pathToResource);
    }
}