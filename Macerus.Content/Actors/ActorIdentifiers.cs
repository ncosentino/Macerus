﻿using System;
using System.Collections.Generic;

using Macerus.Plugins.Features.GameObjects.Actors;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;

namespace Macerus.Content.Actors
{
    public sealed class ActorIdentifiers : IMacerusActorIdentifiers
    {
        private const string DIRECTION_PLACEHOLDER = "$direction$";
        private const string ACTOR_PLACEHOLDER = "$actor$";

        private readonly IReadOnlyDictionary<int, IIdentifier> _directionToAnimationId;

        public ActorIdentifiers()
        {
             _directionToAnimationId = new Dictionary<int, IIdentifier>()
             {
                 [0] = AnimationDirectionLeft,
                 [1] = AnimationDirectionBack,
                 [2] = AnimationDirectionRight,
                 [3] = AnimationDirectionForward,
             };
        }

        public IIdentifier FilterContextActorStatsIdentifier { get; } = new StringIdentifier("actor-stats");

        public IIdentifier ActorTypeIdentifier { get; } = new StringIdentifier("actor");

        public IIdentifier ActorDefinitionIdentifier { get; } = new StringIdentifier("actor-id");

        public IIdentifier AnimationStand { get; } = new StringIdentifier($"{ACTOR_PLACEHOLDER}_stand_{DIRECTION_PLACEHOLDER}");

        public IIdentifier AnimationWalk { get; } = new StringIdentifier($"{ACTOR_PLACEHOLDER}_walk_{DIRECTION_PLACEHOLDER}");

        public IIdentifier AnimationDeath { get; } = new StringIdentifier($"{ACTOR_PLACEHOLDER}_death");

        public IIdentifier AnimationDirectionBack => new StringIdentifier("back");

        public IIdentifier AnimationDirectionForward => new StringIdentifier("forward");

        public IIdentifier AnimationDirectionLeft => new StringIdentifier("left");

        public IIdentifier AnimationDirectionRight => new StringIdentifier("right");

        public IIdentifier AnimationCast { get; } = new StringIdentifier($"{ACTOR_PLACEHOLDER}_cast_{DIRECTION_PLACEHOLDER}");

        public IIdentifier AnimationStrike { get; } = new StringIdentifier($"{ACTOR_PLACEHOLDER}_strike_{DIRECTION_PLACEHOLDER}");

        public IIdentifier AnimationDirectionPlaceholder => new StringIdentifier(DIRECTION_PLACEHOLDER);

        public IIdentifier AnimationActorPlaceholder => new StringIdentifier(ACTOR_PLACEHOLDER);

        public IIdentifier CraftingInventoryIdentifier { get; } = new StringIdentifier("Crafting");

        public IIdentifier InventoryIdentifier { get; } = new StringIdentifier("Inventory");

        public IIdentifier BeltIdentifier { get; } = new StringIdentifier("Belt");

        public IIdentifier MoveDistancePerTurnTotalStatDefinitionId { get; } = new StringIdentifier("stat_70");

        public IIdentifier MoveDistancePerTurnCurrentStatDefinitionId { get; } = new StringIdentifier("stat_71");

        public IIdentifier MoveDiagonallyStatDefinitionId { get; } = new StringIdentifier("stat_73");

        public IIdentifier CurrentExperienceStatDefinitionId { get; } = new StringIdentifier("stat_75");

        public IIdentifier ExperienceForNextLevelStatDefinitionId { get; } = new StringIdentifier("stat_76");

        public IIdentifier LevelStatDefinitionId { get; } = new StringIdentifier("stat_74");

        public IIdentifier SkillPointsStatDefinitionId { get; } = new StringIdentifier("stat_77");

        public IIdentifier AttributePointsStatDefinitionId { get; } = new StringIdentifier("stat_79");

        public IIdentifier AbilityPointsStatDefinitionId { get; } = new StringIdentifier("stat_78");

        public IIdentifier GetAnimationDirectionId(int direction)
        {
            if (_directionToAnimationId.TryGetValue(
                direction,
                out var id))
            {
                return id;
            }

            throw new NotSupportedException(
                $"Unsupported direction value of '{direction}'.");
        }
    }
}
