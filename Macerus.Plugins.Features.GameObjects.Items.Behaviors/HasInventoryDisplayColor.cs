using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Items.Behaviors
{
    public sealed class HasInventoryDisplayColor :
        BaseBehavior,
        IHasInventoryDisplayColor
    {
        public HasInventoryDisplayColor(
            int r,
            int g,
            int b,
            int a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public int R { get; }

        public int G { get; }

        public int B { get; }

        public int A { get; }

        public override string ToString() =>
            $"RGBA({R}, {G}, {B}, {A})";
    }
}