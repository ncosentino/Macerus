using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.GameObjects.Actors.Default.LightRadius
{
    public interface ILightRadiusIdentifiers
    {
        IIdentifier BlueStatIdentifier { get; }
        IIdentifier GreenStatIdentifier { get; }
        IIdentifier IntensityStatIdentifier { get; }
        IIdentifier RadiusStatIdentifier { get; }
        IIdentifier RedStatIdentifier { get; }
    }
}