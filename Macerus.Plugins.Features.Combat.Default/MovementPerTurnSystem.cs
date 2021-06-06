﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Macerus.Plugins.Features.GameObjects.Actors.Api;
using Macerus.Plugins.Features.Stats;

using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.TurnBased.Api;

namespace Macerus.Plugins.Features.Combat.Default
{
    public sealed class MovementPerTurnSystem : IDiscoverableSystem
    {
        private readonly ICombatTurnManager _combatTurnManager;
        private readonly IMacerusActorIdentifiers _macerusActorIdentifiers;
        private readonly IStatCalculationServiceAmenity _statCalculationServiceAmenity;
        private readonly ICombatGameObjectProvider _combatGameObjectProvider;

        public MovementPerTurnSystem(
            ICombatTurnManager combatTurnManager,
            IMacerusActorIdentifiers macerusActorIdentifiers,
            IStatCalculationServiceAmenity statCalculationServiceAmenity,
            ICombatGameObjectProvider combatGameObjectProvider)
        {
            _combatTurnManager = combatTurnManager;
            _macerusActorIdentifiers = macerusActorIdentifiers;
            _statCalculationServiceAmenity = statCalculationServiceAmenity;
            _combatGameObjectProvider = combatGameObjectProvider;

            _combatTurnManager.CombatStarted += CombatTurnManager_CombatStarted;
        }

        public int? Priority => null;

        public void Update(
            ISystemUpdateContext systemUpdateContext,
            IEnumerable<IGameObject> gameObjects)
        {
            var turnInfo = systemUpdateContext
                .GetFirst<IComponent<ITurnInfo>>()
                .Value;
            if (!_combatTurnManager.InCombat || turnInfo.ElapsedTurns != 1)
            {
                return;
            }

            var actors = turnInfo
                .ApplicableGameObjects
                .Where(x =>
                {
                    if (!x.TryGetFirst<ITypeIdentifierBehavior>(out var typeIdentifierBehavior))
                    {
                        return false;
                    }

                    return typeIdentifierBehavior.TypeId.Equals(_macerusActorIdentifiers.ActorTypeIdentifier);
                });
            UpdateStats(actors);
        }

        private void UpdateStats(IEnumerable<IGameObject> actors)
        {
            // FIXME: do later .NET versions have a nicer way to essentially async/await a parallel foreach?
            var tasks = actors
                .Select(actor => Task.Run(async () =>
                {
                    var mutableStats = actor.GetOnly<IHasMutableStatsBehavior>();
                    var calculatedStats = await _statCalculationServiceAmenity.GetStatValuesAsync(
                        actor,
                        new[]
                        {
                            _macerusActorIdentifiers.MoveDistancePerTurnTotalStatDefinitionId,
                        });
                    mutableStats.MutateStats(s =>
                        s[_macerusActorIdentifiers.MoveDistancePerTurnCurrentStatDefinitionId] =
                        calculatedStats[_macerusActorIdentifiers.MoveDistancePerTurnTotalStatDefinitionId]);
                }))
                .ToArray();
            Task.WaitAll(tasks);
        }

        private void CombatTurnManager_CombatStarted(
            object sender, 
            CombatStartedEventArgs e)
        {
            UpdateStats(_combatGameObjectProvider.GetGameObjects());
        }
    }
}
