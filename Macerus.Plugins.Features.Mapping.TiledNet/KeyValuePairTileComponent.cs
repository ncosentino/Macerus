using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.Mapping.TiledNet
{
    public sealed class KeyValuePairTileComponent :
        BaseBehavior,
        IKeyValuePairTileBehavior
    {
        public KeyValuePairTileComponent(string key, object value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; }

        public object Value { get; }
    }
}