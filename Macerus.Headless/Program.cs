
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

using Autofac;

using Macerus.Api.Behaviors;
using Macerus.Api.Behaviors.Filtering;
using Macerus.Plugins.Features.Combat.Api;
using Macerus.Plugins.Features.Encounters;
using Macerus.Plugins.Features.Encounters.SpawnTables.Api;
using Macerus.Plugins.Features.GameObjects.Actors.Api;
using Macerus.Plugins.Features.GameObjects.Actors.Npc;
using Macerus.Plugins.Features.Gui.Api;
using Macerus.Plugins.Features.Interactions.Api;
using Macerus.Plugins.Features.Inventory.Api;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.Logging;
using ProjectXyz.Api.Systems;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Game.Interface.Engine;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Plugins.Features.Mapping.Default.PathFinding;
using ProjectXyz.Plugins.Features.TurnBased.Api;
using ProjectXyz.Shared.Framework;

namespace Macerus.Headless
{
    class Program
    {
        public static void Main(string[] args)
        {
            var container = new MacerusContainer();

            new GameLoopExercise().Go(container);
        }
    }

    public sealed class GameLoopExercise
    {
        public void Go(MacerusContainer container)
        {
            var gameEngine = container.Resolve<IGameEngine>();
            while (true)
            {
                gameEngine.Update();
            }
        }
    }

    public sealed class LootCorpseExercise
    {
        public void Go(MacerusContainer container)
        {
            var gameEngine = container.Resolve<IGameEngine>();

            var actorSpawner = container.Resolve<IActorSpawner>();
            var spawnTableIdentifiers = container.Resolve<ISpawnTableIdentifiers>();
            var mapGameObjectManager = container.Resolve<IMapGameObjectManager>();
            var mapManager = container.Resolve<IMapManager>();
            var filterContextAmenity = container.Resolve<IFilterContextAmenity>();
            var combatStatIdentifiers = container.Resolve<ICombatStatIdentifiers>();
            var turnBasedManager = container.Resolve<ITurnBasedManager>();
            var encounterManager = container.Resolve<IEncounterManager>();
            var logger = container.Resolve<ILogger>();
            var actorIdentifiers = container.Resolve<IMacerusActorIdentifiers>();
            var interactionHandler = container.Resolve<IInteractionHandlerFacade>();

            var filterContext = filterContextAmenity.CreateNoneFilterContext();
            encounterManager.StartEncounter(
                filterContext,
                new StringIdentifier("test-encounter"));

            var player = mapGameObjectManager
                .GameObjects
                .FirstOrDefault(x => x.Has<IPlayerControlledBehavior>());

            var skeleton = mapGameObjectManager
                .GameObjects
                .FirstOrDefault(x =>
                    !x.Has<IPlayerControlledBehavior>() &&
                    x.GetOnly<ITypeIdentifierBehavior>().TypeId.Equals(actorIdentifiers.ActorTypeIdentifier));

            // should be no-op
            interactionHandler.Interact(player, skeleton.GetOnly<CorpseInteractableBehavior>());

            skeleton
                .GetOnly<IHasMutableStatsBehavior>()
                .MutateStats(stats => stats[combatStatIdentifiers.CurrentLifeStatId] = 0);
            
            interactionHandler.Interact(player, skeleton.GetOnly<CorpseInteractableBehavior>());
        }
    }

    public sealed class CombatExercise
    {
        public void Go(MacerusContainer container)
        {
            var gameEngine = container.Resolve<IGameEngine>();

            var actorSpawner = container.Resolve<IActorSpawner>();
            var spawnTableIdentifiers = container.Resolve<ISpawnTableIdentifiers>();
            var mapGameObjectManager = container.Resolve<IMapGameObjectManager>();
            var mapManager = container.Resolve<IMapManager>();
            var filterContextAmenity = container.Resolve<IFilterContextAmenity>();
            var combatTurnManager = container.Resolve<ICombatTurnManager>();
            var turnBasedManager = container.Resolve<ITurnBasedManager>();
            var encounterManager = container.Resolve<IEncounterManager>();
            var logger = container.Resolve<ILogger>();

            var filterContext = filterContextAmenity.CreateNoneFilterContext();
            encounterManager.StartEncounter(
                filterContext,
                new StringIdentifier("test-encounter"));

            var playerInventoryController = container.Resolve<IPlayerInventoryController>();
            playerInventoryController.OpenInventory();

            bool keepRunning = true;
            combatTurnManager.CombatEnded += (s, e) =>
            {
                keepRunning = false;
                logger.Info("Combat complete");
            };

            while (keepRunning)
            {
                gameEngine.Update();
                if (!keepRunning)
                {
                    break;
                }

                var actorWithCurrentTurn = combatTurnManager
                    .GetSnapshot(
                        filterContextAmenity.CreateNoneFilterContext(),
                        1)
                    .Single();
                if (actorWithCurrentTurn.Has<IPlayerControlledBehavior>())
                {
                    turnBasedManager.SetApplicableObjects(new[] { actorWithCurrentTurn });
                    logger.Info("Skipping player turn...");
                }
            }

            Console.ReadLine();
        }
    }

    public sealed class DummyModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<FakeMovementSystem>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }

        private sealed class FakeMovementSystem : IDiscoverableSystem
        {
            private readonly IBehaviorFinder _behaviorFinder;

            public FakeMovementSystem(IBehaviorFinder behaviorFinder)
            {
                _behaviorFinder = behaviorFinder;
            }

            public int? Priority => null;

            public void Update(
                ISystemUpdateContext systemUpdateContext,
                IEnumerable<IHasBehaviors> hasBehaviors)
            {
                var elapsedTime = systemUpdateContext
                    .GetFirst<IComponent<IElapsedTime>>()
                    .Value;
                var elapsedSeconds = ((IInterval<double>)elapsedTime.Interval).Value / 1000;

                foreach (var gameObject in hasBehaviors)
                {
                    if (!_behaviorFinder.TryFind<IReadOnlyMovementBehavior, IWorldLocationBehavior>(
                        gameObject, 
                        out var behaviors))
                    {
                        continue;
                    }

                    var movementBehavior = behaviors.Item1;
                    var locationBehavior = behaviors.Item2;

                    locationBehavior.SetLocation(
                        locationBehavior.X + movementBehavior.VelocityX * elapsedSeconds,
                        locationBehavior.Y + movementBehavior.VelocityY * elapsedSeconds);
                }
            }
        }
    }
}
