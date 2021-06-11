
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
using Macerus.Plugins.Features.GameObjects.Skills.Api;
using Macerus.Plugins.Features.Interactions.Api;
using Macerus.Plugins.Features.Inventory.Api;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Logging;
using ProjectXyz.Api.Systems;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Game.Interface.Engine;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Plugins.Features.TurnBased.Api;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Api.Data.Serialization;
using System.Text;
using System.IO;
using ProjectXyz.Plugins.Features.Behaviors.Default;
using Macerus.Shared.Behaviors;
using Macerus.Plugins.Features.GameObjects.Actors;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using Macerus.Plugins.Features.GameObjects.Actors.Generation;
using ProjectXyz.Api.GameObjects.Generation;
using Macerus.Plugins.Features.StatusBar.Api;

namespace Macerus.Headless
{
    class Program
    {
        public static void Main(string[] args)
        {
            var container = new MacerusContainer();

            new SkillCastExercise().Go(container);
        }
    }

    public sealed class ConvertMapExercise
    {
        public void Go(MacerusContainer container)
        {
            var serializer = container.Resolve<ISerializer>();
            //var filterContextAmenity = container.Resolve<IFilterContextAmenity>();
            //var mapRepositoryFacade = container.Resolve<IMapRepositoryFacade>();
            //var mapGameObjectRepository = container.Resolve<IMapGameObjectRepository>();
            //var filterContext = filterContextAmenity.CreateFilterContextForSingle(
            //    filterContextAmenity.CreateRequiredAttribute(
            //        container.Resolve<IMapIdentifiers>().FilterContextMapIdentifier,
            //        new StringIdentifier("test_encounter_map")));
            //var map = mapRepositoryFacade.LoadMaps(filterContext).Single();

            //using (var outStream = new FileStream("map.json", FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
            //{
            //    serializer.Serialize(outStream, map, Encoding.UTF8);
            //}

            //var gameObjects = mapGameObjectRepository
            //    .LoadForMap(new StringIdentifier("test_encounter_map"))
            //    .ToArray();
            //using (var outStream = new FileStream("map.objects.json", FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
            //{
            //    serializer.Serialize(outStream, gameObjects, Encoding.UTF8);
            //}

            using (var outStream = new FileStream("spawn.json", FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
            {
                serializer.Serialize(outStream, new GameObject(new[] { new SpawnTemplatePropertiesBehavior(new GameObject(new[] { new BoxColliderBehavior(1, 2, 3, 4, true) })) }), Encoding.UTF8);
            }
        }
    }

    public sealed class SwitchMapExercise
    {
        public void Go(MacerusContainer container)
        {
            var mapManager = container.Resolve<IMapManager>();
            mapManager.SwitchMap(new StringIdentifier("swamp"));
            mapManager.SwitchMap(new StringIdentifier("swamp"));
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

            // FIXME: this is just a hack to spawn the player
            mapManager.SwitchMap(new StringIdentifier("swamp"));

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

    public sealed class SkillCastExercise
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
            var skillAmenity = container.Resolve<ISkillAmenity>();
            var skillTargetingAmenity = container.Resolve<ISkillTargetingAmenity>();
            var skillUsage = container.Resolve<ISkillUsage>();
            var skillHandlerFacade = container.Resolve<ISkillHandlerFacade>();
            var logger = container.Resolve<ILogger>();
            var gameObjectIdentifiers = container.Resolve<IGameObjectIdentifiers>();
            var actorIdentifiers = container.Resolve<IActorIdentifiers>();
            var actorGeneratorFacade = container.Resolve<IActorGeneratorFacade>();
            var statusBarController = container.Resolve<IStatusBarController>();

            // FIXME: this is just a hack to spawn the player
            mapManager.SwitchMap(new StringIdentifier("swamp"));
            var context = filterContextAmenity.CreateFilterContextForSingle(
                filterContextAmenity.CreateRequiredAttribute(
                    gameObjectIdentifiers.FilterContextTypeId,
                    actorIdentifiers.ActorTypeIdentifier),
                filterContextAmenity.CreateRequiredAttribute(
                    actorIdentifiers.ActorDefinitionIdentifier,
                    new StringIdentifier("player")));
            var player = actorGeneratorFacade
                .GenerateActors(
                    context,
                    new IGeneratorComponent[0])
                .Single();
            player.GetOnly<IPositionBehavior>().SetPosition(40, -16);
            mapGameObjectManager.MarkForAddition(player);

            var filterContext = filterContextAmenity.CreateNoneFilterContext();
            encounterManager.StartEncounter(
                filterContext,
                new StringIdentifier("test-encounter"));

            var actors = mapGameObjectManager
                .GameObjects
                .Where(x => x
                    .Get<ITypeIdentifierBehavior>()
                    .Any(y => y.TypeId.Equals(new StringIdentifier("actor"))));

            var target = actors
                .Where(x => !x.Has<IPlayerControlledBehavior>())
                .FirstOrDefault();

            if (target == null)
            {
                logger.Info("No target!");
                return;
            }
            else
            {
                var targetLocation = target.GetOnly<IPositionBehavior>();
                var playerLocation = player.GetOnly<IPositionBehavior>();
                var playerMovement = player.GetOnly<IMovementBehavior>();

                logger.Info($"Player targets the enemy at: ({targetLocation.X}, {targetLocation.Y})");

                playerMovement.SetWalkPath(new[] {
                    new Vector2((float)targetLocation.X, (float)targetLocation.Y - 1),
                });

                logger.Info($"Player path set to end at: ({targetLocation.X}, {targetLocation.Y - 1})");
            }

            var keepRunning = true;
            while (keepRunning)
            {
                gameEngine.Update();
                if (!keepRunning)
                {
                    break;
                }

                var playerMovement = player.GetOnly<IMovementBehavior>();
                var playerLocation = player.GetOnly<IPositionBehavior>();
                if (playerMovement.PointsToWalk.Count < 1)
                {
                    var skill = skillAmenity.GetSkillById(new StringIdentifier("fireball"));

                    playerMovement.SetDirection(1);
                    logger.Info($"Player turned to direction {playerMovement.Direction} to cast");
                    skillHandlerFacade.Handle(player, skill);

                    keepRunning = false;
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
                IEnumerable<IGameObject> gameObjects)
            {
                var elapsedTime = systemUpdateContext
                    .GetFirst<IComponent<IElapsedTime>>()
                    .Value;
                var elapsedSeconds = ((IInterval<double>)elapsedTime.Interval).Value / 1000;

                foreach (var gameObject in gameObjects)
                {
                    if (!_behaviorFinder.TryFind<IReadOnlyMovementBehavior, IPositionBehavior>(
                        gameObject, 
                        out var behaviors))
                    {
                        continue;
                    }

                    var movementBehavior = behaviors.Item1;
                    var locationBehavior = behaviors.Item2;

                    locationBehavior.SetPosition(
                        locationBehavior.X + movementBehavior.VelocityX * elapsedSeconds,
                        locationBehavior.Y + movementBehavior.VelocityY * elapsedSeconds);
                }
            }
        }
    }
}
