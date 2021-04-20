using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Items.Behaviors
{
    public sealed class HasInventoryIconColor :
        BaseBehavior,
        IHasInventoryIconColor
    {
        public HasInventoryIconColor(
            float iconOpacity,
            int r,
            int g,
            int b,
            int a)
        {
            IconOpacity = iconOpacity;
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public float IconOpacity { get; }

        public int R { get; }

        public int G { get; }

        public int B { get; }

        public int A { get; }

        public override string ToString() =>
            $"Opacity={IconOpacity}, RGBA({R}, {G}, {B}, {A})";
    }
}