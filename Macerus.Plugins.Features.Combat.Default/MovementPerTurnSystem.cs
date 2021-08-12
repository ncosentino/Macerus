using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.GameObjects.Actors;
using Macerus.Plugins.Features.Mapping;
using Macerus.Plugins.Features.Stats;

using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Plugins.Features.TurnBased;

namespace Macerus.Plugins.Features.Combat.Default
{
    public sealed class MovementPerTurnSystem : IDiscoverableSystem
    {
        private readonly ICombatTurnManager _combatTurnManager;
        private readonly IMacerusActorIdentifiers _macerusActorIdentifiers;
        private readonly IStatCalculationServiceAmenity _statCalculationServiceAmenity;
        private readonly ICombatGameObjectProvider _combatGameObjectProvider;
        private readonly IMappingAmenity _mappingAmenity;
        private readonly IMapTraversableHighlighter _mapTraversableHighlighter;
        private readonly IFilterContextProvider _filterContextProvider;

        private IObservableMovementBehavior _trackedMovementBehavior;
        private bool _previouslyTrackedMovement;

        public MovementPerTurnSystem(
            ICombatTurnManager combatTurnManager,
            IMacerusActorIdentifiers macerusActorIdentifiers,
            IStatCalculationServiceAmenity statCalculationServiceAmenity,
            ICombatGameObjectProvider combatGameObjectProvider,
            IMappingAmenity mappingAmenity,
            IMapTraversableHighlighter mapTraversableHighlighter,
            IFilterContextProvider filterContextProvider)
        {
            _combatTurnManager = combatTurnManager;
            _macerusActorIdentifiers = macerusActorIdentifiers;
            _statCalculationServiceAmenity = statCalculationServiceAmenity;
            _combatGameObjectProvider = combatGameObjectProvider;
            _mappingAmenity = mappingAmenity;
            _mapTraversableHighlighter = mapTraversableHighlighter;
            _filterContextProvider = filterContextProvider;

            _combatTurnManager.CombatStarted += CombatTurnManager_CombatStarted;
            _combatTurnManager.CombatEnded += CombatTurnManager_CombatEnded;
        }

        public int? Priority => null;

        public async Task UpdateAsync(ISystemUpdateContext systemUpdateContext)
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
            UpdateAndTrackTraversableHighlighting(_combatTurnManager
                .GetSnapshot(_filterContextProvider.GetContext(), 1)
                .Single());
        }

        private void UpdateStats(IEnumerable<IGameObject> actors)
        {
            // FIXME: do later .NET versions have a nicer way to essentially async/await a parallel foreach?
            var tasks = actors
                .Select(actor => Task.Run(async () =>
                {
                    var mutableStats = actor.GetOnly<IHasStatsBehavior>();
                    var calculatedStats = await _statCalculationServiceAmenity.GetStatValuesAsync(
                        actor,
                        new[]
                        {
                            _macerusActorIdentifiers.MoveDistancePerTurnTotalStatDefinitionId,
                        });
                    await mutableStats.MutateStatsAsync(async s =>
                        s[_macerusActorIdentifiers.MoveDistancePerTurnCurrentStatDefinitionId] =
                        calculatedStats[_macerusActorIdentifiers.MoveDistancePerTurnTotalStatDefinitionId]);
                }))
                .ToArray();
            Task.WaitAll(tasks);
        }

        private void UpdateAndTrackTraversableHighlighting(IGameObject actor)
        {
            if (_trackedMovementBehavior != null)
            {
                _trackedMovementBehavior.ThrottleChanged -= TrackedMovementBehavior_ThrottleChanged;
            }

            UpdateTraversableHighlighting(actor);

            _trackedMovementBehavior = actor?.GetOnly<IObservableMovementBehavior>();
            _previouslyTrackedMovement = false;

            if (_trackedMovementBehavior != null)
            {
                _trackedMovementBehavior.ThrottleChanged += TrackedMovementBehavior_ThrottleChanged;
            }
        }

        private void UpdateTraversableHighlighting(IGameObject actor)
        {
            var traversablePoints = actor?.Has<IPlayerControlledBehavior>() == true
                ? _mappingAmenity.GetAllowedPathDestinationsForActor(actor)
                : Enumerable.Empty<Vector2>();
            _mapTraversableHighlighter.SetTraversableTiles(traversablePoints);
            _mapTraversableHighlighter.SetTargettedTiles(new Dictionary<int, HashSet<Vector2>>());
        }

        private void TrackedMovementBehavior_ThrottleChanged(
            object sender,
            EventArgs e)
        {
            var movementBehavior = (IReadOnlyMovementBehavior)sender;
            bool movement =
                Math.Abs(movementBehavior.ThrottleX) > 0 ||
                Math.Abs(movementBehavior.ThrottleY) > 0;
            
            // if we weren't moving but now we are, clear the movement highlighting
            // but otherwise if we were moving and now we aren't, set the movement highlighting
            if (!_previouslyTrackedMovement && movement)
            {
                UpdateTraversableHighlighting(null);
            }
            else if (_previouslyTrackedMovement && !movement)
            {
                UpdateTraversableHighlighting(movementBehavior.Owner);
            }

            _previouslyTrackedMovement = movement;
        }

        private void CombatTurnManager_CombatStarted(
            object sender, 
            CombatStartedEventArgs e)
        {
            UpdateStats(_combatGameObjectProvider.GetGameObjects());
            UpdateAndTrackTraversableHighlighting(e.ActorOrder.First());
        }

        private void CombatTurnManager_CombatEnded(
            object sender,
            CombatEndedEventArgs e)
        {
            UpdateAndTrackTraversableHighlighting(null);
        }
    }
}
