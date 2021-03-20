namespace Macerus.Plugins.Features.Mapping.TiledNet
{
    public sealed class TileResourceComponent : ITileResourceComponent
    {
        public TileResourceComponent(
            string tilesetResourcePath,
            string spriteResourceName)
        {
            TilesetResourcePath = tilesetResourcePath;
            SpriteResourceName = spriteResourceName;
        }

        public string TilesetResourcePath { get; }

        public string SpriteResourceName { get; }
    }
}