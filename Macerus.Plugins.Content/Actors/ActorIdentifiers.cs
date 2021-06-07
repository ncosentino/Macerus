using Macerus.Plugins.Features.GameObjects.Actors.Api;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Content.Actors
{
    public sealed class ActorIdentifiers : IMacerusActorIdentifiers
    {
        public IIdentifier FilterContextActorStatsIdentifier { get; } = new StringIdentifier("actor-stats");

        public IIdentifier ActorTypeIdentifier { get; } = new StringIdentifier("actor");

        public IIdentifier ActorDefinitionIdentifier { get; } = new StringIdentifier("actor-id");

        public IIdentifier AnimationStandBack { get; } = new StringIdentifier("$actor$_stand_back");

        public IIdentifier AnimationStandForward { get; } = new StringIdentifier("$actor$_stand_forward");

        public IIdentifier AnimationStandLeft { get; } = new StringIdentifier("$actor$_stand_left");

        public IIdentifier AnimationStandRight { get; } = new StringIdentifier("$actor$_stand_right");

        public IIdentifier AnimationWalkBack { get; } = new StringIdentifier("$actor$_walk_back");

        public IIdentifier AnimationWalkForward { get; } = new StringIdentifier("$actor$_walk_forward");

        public IIdentifier AnimationWalkLeft { get; } = new StringIdentifier("$actor$_walk_left");

        public IIdentifier AnimationWalkRight { get; } = new StringIdentifier("$actor$_walk_right");

        public IIdentifier AnimationDeath { get; } = new StringIdentifier("$actor$_death");

        public IIdentifier AnimationCastBack { get; } = new StringIdentifier("$actor$_cast_back");

        public IIdentifier AnimationCastForward { get; } = new StringIdentifier("$actor$_cast_forward");

        public IIdentifier AnimationCastLeft { get; } = new StringIdentifier("$actor$_cast_left");

        public IIdentifier AnimationCastRight { get; } = new StringIdentifier("$actor$_cast_right");

        public IIdentifier CraftingInventoryIdentifier { get; } = new StringIdentifier("Crafting");

        public IIdentifier InventoryIdentifier { get; } = new StringIdentifier("Inventory");

        public IIdentifier BeltIdentifier { get; } = new StringIdentifier("Belt");

        public IIdentifier MoveDistancePerTurnTotalStatDefinitionId { get; } = new StringIdentifier("total move distance per turn");

        public IIdentifier MoveDistancePerTurnCurrentStatDefinitionId { get; } = new StringIdentifier("current move distance per turn");

        public IIdentifier MoveDiagonallyStatDefinitionId { get; } = new StringIdentifier("move diagonally");
    }
}
