using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;

namespace Macerus.Plugins.Features.GameObjects.Actors
{
    public interface IMacerusActorIdentifiers : IActorIdentifiers
    {
        IIdentifier InventoryIdentifier { get; }

        IIdentifier CraftingInventoryIdentifier { get; }

        IIdentifier BeltIdentifier { get; }

        IIdentifier MoveDistancePerTurnTotalStatDefinitionId { get; }

        IIdentifier MoveDistancePerTurnCurrentStatDefinitionId { get; }

        IIdentifier MoveDiagonallyStatDefinitionId { get; }

        IIdentifier AnimationCast { get; }

        IIdentifier AnimationStrike { get; }

        IIdentifier AnimationActorPlaceholder { get; }

        IIdentifier GetAnimationDirectionId(int direction);

        IIdentifier CurrentExperienceStatDefinitionId { get; }

        IIdentifier ExperienceForNextLevelStatDefinitionId { get; }

        IIdentifier LevelStatDefinitionId { get; }

        IIdentifier SkillPointsStatDefinitionId { get; }

        IIdentifier AttributePointsStatDefinitionId { get; }

        IIdentifier AbilityPointsStatDefinitionId { get; }
    }
}
