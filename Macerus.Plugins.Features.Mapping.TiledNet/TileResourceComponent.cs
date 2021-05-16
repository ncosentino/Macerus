using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.Mapping.TiledNet
{
    public sealed class TileResourceComponent :
        BaseBehavior,
        ITileResourceBehavior
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