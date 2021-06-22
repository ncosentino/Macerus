using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Mapping;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Content.Mapping
{
    public sealed class MapIdentifiers : IMapIdentifiers
    {
        public IIdentifier FilterContextMapIdentifier { get; } = new StringIdentifier("id");
    }
}
