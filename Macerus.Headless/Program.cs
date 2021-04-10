
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

using Autofac;

using Macerus.Api.Behaviors;
using Macerus.Api.Behaviors.Filtering;
using Macerus.Plugins.Features.Encounters;
using Macerus.Plugins.Features.Encounters.SpawnTables.Api;

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

            //var filterContext = filterContextAmenity.CreateFilterContextForSingle(
            //    filterContextAmenity.CreateRequiredAttribute(
            //        spawnTableIdentifiers.FilterContextSpawnTableIdentifier,
            //        new StringIdentifier("test-multi-skeleton")));
            //filterContext = filterContextAmenity.CopyWithRange(filterContext, 2, 2);

            //mapGameObjectManager.MarkForAddition(actorSpawner.SpawnActors(filterContext, new IGeneratorComponent[] { }));
            //mapGameObjectManager.Synchronize();

            //combatTurnManager.StartCombat(filterContext);

            var filterContext = filterContextAmenity.CreateNoneFilterContext();
            encounterManager.StartEncounter(
                filterContext,
                new StringIdentifier("test-encounter"));

            //var actor1LocationBehavior = mapGameObjectManager
            //    .GameObjects
            //    .First(x => x.Has<IPlayerControlledBehavior>())
            //    .GetOnly<IReadOnlyWorldLocationBehavior>();
            //var actor2LocationBehavior = mapGameObjectManager
            //    .GameObjects.First(x => 
            //        x.GetOnly<ITypeIdentifierBehavior>().TypeId.Equals(new StringIdentifier("actor")) &&
            //        !x.Has<IPlayerControlledBehavior>())
            //    .GetOnly<IReadOnlyWorldLocationBehavior>();

            //var startingPosition = new Vector2((float)actor1LocationBehavior.X, (float)actor1LocationBehavior.Y);

            //var adjacentPositions = mapManager
            //    .PathFinder
            //    .GetAdjacentPositions(
            //        new Vector2((int)actor2LocationBehavior.X, (int)actor2LocationBehavior.Y), // FIXME: note how this is INTEGER-based
            //        true)
            //    .ToArray();
            //var destination = ClosestPosition(
            //    startingPosition,
            //    adjacentPositions);
            //var path = mapManager
            //    .PathFinder
            //    .FindPath(
            //        startingPosition,
            //        destination,
            //        new Vector2((float)actor1LocationBehavior.Width, (float)actor1LocationBehavior.Height))
            //    .ToArray();
            //Console.WriteLine(
            //    $"Path from player ({actor1LocationBehavior.X},{actor1LocationBehavior.Y}) " +
            //    $"to enemy ({actor2LocationBehavior.X},{actor2LocationBehavior.Y}) is:");
            //foreach (var point in path)
            //{
            //    Console.WriteLine($"\t({point.X},{point.Y})");
            //}

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

        private static void CombatTurnManager_CombatEnded(object sender, CombatEndedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static Vector2 ClosestPosition(
            Vector2 source,
            IEnumerable<Vector2> candidates)
        {
            var closest = candidates
                .OrderBy(x => Math.Abs(source.X - x.X) + Math.Abs(source.Y - x.Y))
                .First();
            return closest;
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
