using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;

namespace Macerus.Plugins.Features.GameObjects.Actors.Api
{
    public interface IMacerusActorIdentifiers : IActorIdentifiers
    {
        IIdentifier InventoryIdentifier { get; }

        IIdentifier CraftingInventoryIdentifier { get; }

        IIdentifier BeltIdentifier { get; }

        IIdentifier MoveDistancePerTurnTotalStatDefinitionId { get; }

        IIdentifier MoveDistancePerTurnCurrentStatDefinitionId { get; }

        IIdentifier MoveDiagonallyStatDefinitionId { get; }
    }
}
