using System;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using Autofac;

using Macerus.Api.Behaviors;
using Macerus.Api.Behaviors.Filtering;
using Macerus.Plugins.Features.Combat.Api;
using Macerus.Plugins.Features.DataPersistence;
using Macerus.Plugins.Features.Encounters;
using Macerus.Plugins.Features.Encounters.SpawnTables;
using Macerus.Plugins.Features.GameObjects.Actors;
using Macerus.Plugins.Features.GameObjects.Actors.Default;
using Macerus.Plugins.Features.GameObjects.Actors.Default.AI;
using Macerus.Plugins.Features.GameObjects.Actors.Generation;
using Macerus.Plugins.Features.GameObjects.Skills;
using Macerus.Plugins.Features.Interactions.Api;
using Macerus.Plugins.Features.MainMenu.Default.NewGame;
using Macerus.Plugins.Features.Scripting;
using Macerus.Plugins.Features.StatusBar.Api;
using Macerus.Shared.Behaviors;

using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.Logging;
using ProjectXyz.Game.Api;
using ProjectXyz.Game.Interface.Engine;
using ProjectXyz.Plugins.Features.Behaviors.Default;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Plugins.Features.Mapping;
using ProjectXyz.Plugins.Features.PartyManagement;
using ProjectXyz.Plugins.Features.TurnBased;
using ProjectXyz.Shared.Framework;

namespace Macerus.Headless
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var container = new MacerusContainer();
            await new SkillCastExercise().Go(container);
        }
    }

    public sealed class ScriptExercise
    {
        public async Task Go(MacerusContainer container)
        {
            string code = @"
    using System;
    using System.Threading.Tasks;

    using ProjectXyz.Api.Framework;
    using ProjectXyz.Shared.Framework;

    using Macerus.Headless;
    using Macerus.Plugins.Features.Scripting;
    using Macerus.Plugins.Features.GameObjects.Skills;

    namespace The.Name.Space
    {
        public class MyScript : IScript
        {
            private readonly Params _parameters;
            private int _counter = 0;

            public MyScript(Params parameters)
            {
                _parameters = parameters;
            }

            public async Task RunAsync()
            {
                var isSet = _parameters.SkillAmenity != null;
                var identifier = new StringIdentifier(""Params Has Property Set: "" + isSet);
                Console.WriteLine(identifier.ToString());

                _counter++;
                Console.WriteLine(""Counter: "" + _counter);
            }
        }

        public class Params : IScriptConstructorParameters
        {
            public ISkillAmenity SkillAmenity { get; set; }
        }
    }
";

            var scriptCompiler = container.Resolve<IScriptCompiler>();
            var script = await scriptCompiler
                .CompileFromRawAsync(code, "The.Name.Space.MyScript", false)
                .ConfigureAwait(false);
            await script
                .RunAsync()
                .ConfigureAwait(false);
            await script
                .RunAsync()
                .ConfigureAwait(false);
            Console.ReadLine();
        }
    }

    public sealed class NpcAIExercise
    {
        public async Task Go(MacerusContainer container)
        {
            var npc = container.Resolve<IGameObjectFactory>().Create(new IBehavior[]
            {
                new PositionBehavior(40, -16),
                new SizeBehavior(0, 0),
                new MovementBehavior(),
                container.Resolve<IDynamicAnimationBehaviorFactory>().Create(
                    new StringIdentifier(string.Empty),
                    true,
                    0),
                new AIBehavior(),
                new WalkZoneAITaskBehavior(1, 40, -16, 10, 2),
                new IdleAITaskBehavior(1, TimeSpan.Zero, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2)),
            });
            var movementBehavior = npc.GetOnly<IMovementBehavior>();
            movementBehavior.Direction = 3;
            npc.GetOnly<IDynamicAnimationBehavior>().BaseAnimationId = container.Resolve<IMacerusActorIdentifiers>().AnimationStand;

            var mapManager = container.Resolve<IMapManager>();
            await mapManager.SwitchMapAsync(new StringIdentifier("swamp"));

            var mapGameObjectManager = container.Resolve<IMapGameObjectManager>();
            mapGameObjectManager.MarkForAddition(npc);
            await mapGameObjectManager
                .SynchronizeAsync()
                .ConfigureAwait(false);

            var gameEngine = container.Resolve<IGameEngine>();
            while (true)
            {
                await gameEngine.UpdateAsync();
            }
        }
    }

    public sealed class AnimateExercise
    {
        public async Task Go(MacerusContainer container)
        {
            var player = CreatePlayerInstance(container);
            var movementBehavior = player.GetOnly<IMovementBehavior>();
            movementBehavior.Direction = 3;
            player.GetOnly<IDynamicAnimationBehavior>().BaseAnimationId = container.Resolve<IMacerusActorIdentifiers>().AnimationStand;

            container.Resolve<IGameObjectRepository>().Save(player);
            container.Resolve<IRosterManager>().AddToRoster(player);
            player.GetOnly<IRosterBehavior>().IsPartyLeader = true;

            var mapManager = container.Resolve<IMapManager>();
            await mapManager.SwitchMapAsync(new StringIdentifier("swamp"));

            var gameEngine = container.Resolve<IGameEngine>();
            while (true)
            {
                movementBehavior.SetThrottle(1, 0);
                await gameEngine.UpdateAsync();
            }
        }

        private IGameObject CreatePlayerInstance(MacerusContainer container)
        {
            var filterContextAmenity = container.Resolve<IFilterContextAmenity>();
            var actorIdentifiers = container.Resolve<IMacerusActorIdentifiers>();
            var gameObjectIdentifiers = container.Resolve<IGameObjectIdentifiers>();
            var actorGeneratorFacade = container.Resolve<IActorGeneratorFacade>();
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
                    new IGeneratorComponent[] { })
                .Single();
            return player;
        }
    }

    public sealed class DataPersistenceExercise
    {
        public async Task Go(MacerusContainer container)
        {
            var player = CreatePlayerInstance(container);
            player.GetOnly<IPositionBehavior>().SetPosition(40, -16);
            container.Resolve<IGameObjectRepository>().Save(player);
            container.Resolve<IRosterManager>().AddToRoster(player);
            player.GetOnly<IRosterBehavior>().IsPartyLeader = true;

            var mapManager = container.Resolve<IMapManager>();
            await mapManager.SwitchMapAsync(new StringIdentifier("swamp"));

            var dataPersistenceManager = container.Resolve<IDataPersistenceManager>();
            await dataPersistenceManager.SaveAsync(new StringIdentifier("my save game"));
            await dataPersistenceManager.LoadAsync(new StringIdentifier("my save game"));
        }

        private IGameObject CreatePlayerInstance(MacerusContainer container)
        {
            var filterContextAmenity = container.Resolve<IFilterContextAmenity>();
            var actorIdentifiers = container.Resolve<IMacerusActorIdentifiers>();
            var gameObjectIdentifiers = container.Resolve<IGameObjectIdentifiers>();
            var actorGeneratorFacade = container.Resolve<IActorGeneratorFacade>();
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
                    new IGeneratorComponent[] { })
                .Single();
            return player;
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
        public async Task Go(MacerusContainer container)
        {
            var mapManager = container.Resolve<IMapManager>();
            await mapManager.SwitchMapAsync(new StringIdentifier("swamp"));
            await mapManager.SwitchMapAsync(new StringIdentifier("swamp"));
        }
    }

    public sealed class GameLoopExercise
    {
        public async Task Go(MacerusContainer container)
        {
            var gameEngine = container.Resolve<IGameEngine>();
            while (true)
            {
                await gameEngine.UpdateAsync();
            }
        }
    }

    public sealed class LootCorpseExercise
    {
        public async Task Go(MacerusContainer container)
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
            await encounterManager.StartEncounterAsync(
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
            await interactionHandler.InteractAsync(player, skeleton.GetOnly<CorpseInteractableBehavior>());

            await skeleton
                .GetOnly<IHasStatsBehavior>()
                .MutateStatsAsync(async stats => stats[combatStatIdentifiers.CurrentLifeStatId] = 0)
                .ConfigureAwait(false);
            
            await interactionHandler.InteractAsync(player, skeleton.GetOnly<CorpseInteractableBehavior>());
        }
    }

    public sealed class CombatExercise
    {
        public async Task Go(MacerusContainer container)
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
            var newGameWorkflow = container.Resolve<INewGameWorkflow>();

            // set ourselves up with a new game!
            await newGameWorkflow.RunAsync();

            var filterContext = filterContextAmenity.CreateNoneFilterContext();
            await encounterManager.StartEncounterAsync(
                filterContext,
                new StringIdentifier("test-encounter"));

            bool keepRunning = true;
            combatTurnManager.CombatEnded += (s, e) =>
            {
                keepRunning = false;
                logger.Info("Combat complete");
            };

            while (keepRunning)
            {
                await gameEngine.UpdateAsync();
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
                    turnBasedManager.NotifyTurnTaken(actorWithCurrentTurn);
                    logger.Info("Skipping player turn...");
                }
            }

            Console.ReadLine();
        }
    }

    public sealed class SkillCastExercise
    {
        public async Task Go(MacerusContainer container)
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
            await mapManager.SwitchMapAsync(new StringIdentifier("swamp"));
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
            await encounterManager.StartEncounterAsync(
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
                    new Vector2((float)playerLocation.X, (float)playerLocation.Y),
                    new Vector2((float)targetLocation.X, (float)targetLocation.Y - 1),
                });

                logger.Info($"Player path set to end at: ({targetLocation.X}, {targetLocation.Y - 1})");
            }

            var keepRunning = true;
            while (keepRunning)
            {
                await gameEngine.UpdateAsync();
                if (!keepRunning)
                {
                    break;
                }

                var playerMovement = player.GetOnly<IMovementBehavior>();
                var playerLocation = player.GetOnly<IPositionBehavior>();
                if (playerMovement.PointsToWalk.Count < 1)
                {
                    var skill = skillAmenity.GetSkillById(new StringIdentifier("fireball"));

                    playerMovement.Direction = 1;
                    logger.Info($"Player turned to direction {playerMovement.Direction} to cast");
                    await skillHandlerFacade
                        .HandleSkillAsync(player, skill)
                        .ConfigureAwait(false);

                    keepRunning = false;
                }
            }

            Console.ReadLine();
        }
    }
}
