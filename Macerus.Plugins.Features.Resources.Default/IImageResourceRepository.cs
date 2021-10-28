using System.IO;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Resources.Default
{
    public interface IImageResourceRepository
    {
        Stream OpenStreamForResource(IIdentifier imageResourceId);
    }
}