using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.Mapping
{
    public sealed class TileResourceBehavior :
        BaseBehavior,
        ITileResourceBehavior
    {
        public TileResourceBehavior(
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