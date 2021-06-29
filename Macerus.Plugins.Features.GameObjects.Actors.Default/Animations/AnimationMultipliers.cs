using Macerus.Api.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Actors.Default.Animations
{
    public sealed class AnimationMultipliers : IAnimationMultipliers
    {
        public AnimationMultipliers(
            double animationSpeedMultiplier,
            double redMultiplier,
            double greenMultiplier,
            double blueMultiplier,
            double alphaMultiplier)
        {
            AnimationSpeedMultiplier = animationSpeedMultiplier;
            RedMultiplier = redMultiplier;
            GreenMultiplier = greenMultiplier;
            BlueMultiplier = blueMultiplier;
            AlphaMultiplier = alphaMultiplier;
        }

        public double AnimationSpeedMultiplier { get; }

        public double RedMultiplier { get; }

        public double GreenMultiplier { get; }

        public double BlueMultiplier { get; }

        public double AlphaMultiplier { get; }
    }
}
