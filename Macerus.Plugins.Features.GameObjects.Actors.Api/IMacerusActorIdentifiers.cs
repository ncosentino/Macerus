using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;

namespace Macerus.Plugins.Features.GameObjects.Actors.Api
{
    public interface IMacerusActorIdentifiers : IActorIdentifiers
    {
        IIdentifier InventoryIdentifier { get; }

        IIdentifier BeltIdentifier { get; }
    }
}
