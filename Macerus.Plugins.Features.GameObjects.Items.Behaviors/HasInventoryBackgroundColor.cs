using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Items.Behaviors
{
    public sealed class HasInventoryBackgroundColor :
        BaseBehavior,
        IHasInventoryBackgroundColor
    {
        public HasInventoryBackgroundColor(
            int r,
            int g,
            int b)
        {
            R = r;
            G = g;
            B = b;
        }

        public int R { get; }

        public int G { get; }

        public int B { get; }

        public override string ToString() =>
            $"RGB({R}, {G}, {B})";
    }
}