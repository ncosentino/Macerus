
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.GameObjects.Actors
{
    public interface IDynamicAnimationIdentifiers
    {
        IIdentifier AlphaMultiplierStatId { get; }
        
        IIdentifier AnimationSpeedMultiplierStatId { get; }
        
        IIdentifier BlueMultiplierStatId { get; }
        
        IIdentifier GreenMultiplierStatId { get; }
        
        IIdentifier RedMultiplierStatId { get; }

        IIdentifier AnimationOverrideStatId { get; }
    }
}
