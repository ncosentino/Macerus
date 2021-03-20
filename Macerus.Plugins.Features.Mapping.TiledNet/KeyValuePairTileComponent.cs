namespace Macerus.Plugins.Features.Mapping.TiledNet
{
    public sealed class KeyValuePairTileComponent : IKeyValuePairTileComponent
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