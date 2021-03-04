using ProjectXyz.Api.Framework;

namespace Macerus.Api.Behaviors
{
    public interface IReadOnlyDynamicAnimationBehavior : IReadOnlyAnimationBehavior
    {
        IIdentifier BaseAnimationId { get; }

        double? AnimationSpeedMultiplier { get; }

        double? RedMultiplier { get; }

        double? GreenMultiplier { get; }

        double? BlueMultiplier { get; }
        
        double? AlphaMultiplier { get; }
    }
}