
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.GameObjects.Actors.Api
{
    public interface IDynamicAnimationIdentifiers
    {
        IIdentifier AlphaMultiplierStatId { get; }
        IIdentifier AnimationSpeedMultiplierStatId { get; }
        IIdentifier BlueMultiplierStatId { get; }
        IIdentifier GreenMultiplierStatId { get; }
        IIdentifier RedMultiplierStatId { get; }
    }
}
