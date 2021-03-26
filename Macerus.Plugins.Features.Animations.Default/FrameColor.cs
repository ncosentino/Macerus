
using Macerus.Plugins.Features.Animations.Api;

namespace Macerus.Plugins.Features.Animations.Default
{
    public sealed class FrameColor : IFrameColor
    {
        public FrameColor(
            double red,
            double green,
            double blue,
            double alpha)
        {
            Red = red;
            Green = green;
            Blue = blue;
            Alpha = alpha;
        }

        public double Red { get; }

        public double Green { get; }

        public double Blue { get; }

        public double Alpha { get; }
    }
}
