namespace Macerus.Api.Behaviors
{
    public interface IAnimationMultipliers
    {
        double AnimationSpeedMultiplier { get; }

        double RedMultiplier { get; }

        double GreenMultiplier { get; }

        double BlueMultiplier { get; }

        double AlphaMultiplier { get; }
    }
}