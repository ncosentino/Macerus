using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Plugins.Features.Mapping.TiledNet
{
    public interface IKeyValuePairTileBehavior : IBehavior
    {
        string Key { get; }

        object Value { get; }
    }
}