using System.Linq;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.GameObjects.Containers;
using Macerus.Plugins.Features.GameObjects.Containers.Api;
using Macerus.Plugins.Features.GameObjects.Static.Doors;

using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Mapping;
using ProjectXyz.Shared.Framework;

using Xunit;

namespace Macerus.Tests.Plugins.Features.Mapping
{
    public sealed class MapGameObjectRepositoryTests
    {
        private const int OBJ_COUNT_ON_TESTENCOUNTERMAP = 15;

        private static readonly MacerusContainer _container;
        private static readonly TestAmenities _testAmenities;
        private static readonly IMapGameObjectRepository _mapGameObjectRepository;

        static MapGameObjectRepositoryTests()
        {
            _container = new MacerusContainer();
            _testAmenities = new TestAmenities(_container);

            _mapGameObjectRepository = _container.Resolve<IMapGameObjectRepository>();
        }

        [Fact]
        private void LoadForMap_TestEncounterMap_WallHasExpectedBehaviors()
        {
            _testAmenities.UsingCleanMapAndObjects(async () =>
            {
                var gameObjects = (await _mapGameObjectRepository
                    .LoadForMapAsync(new StringIdentifier("test_encounter_map")))
                    .ToArray();

                Assert.Equal(OBJ_COUNT_ON_TESTENCOUNTERMAP, gameObjects.Length);

                var wallBehaviors = gameObjects[0].Behaviors;

                Assert.Equal(8, wallBehaviors.Count);
                Assert.Equal(
                    new StringIdentifier("static"),
                    wallBehaviors
                        .TakeTypes<IReadOnlyTypeIdentifierBehavior>()
                        .Single()
                        .TypeId);
                Assert.Equal(
                    new StringIdentifier("static/wall"),
                    wallBehaviors
                        .TakeTypes<IReadOnlyTemplateIdentifierBehavior>()
                        .Single()
                        .TemplateId);
                Assert.Equal(
                    new StringIdentifier("static/wall"),
                    wallBehaviors
                        .TakeTypes<ICreatedFromTemplateBehavior>()
                        .Single()
                        .TemplateId);
                Assert.Equal(
                    new StringIdentifier("Mapping/Prefabs/static/wall"),
                    wallBehaviors
                        .TakeTypes<IReadOnlyPrefabResourceIdBehavior>()
                        .Single()
                        .PrefabResourceId);
                Assert.Equal(
                    new StringIdentifier("static--8a27dfc3-7901-436c-a450-b04633f4d8bd"),
                    wallBehaviors
                        .TakeTypes<IReadOnlyIdentifierBehavior>()
                        .Single()
                        .Id);
                Assert.Equal(
                    40,
                    wallBehaviors
                        .TakeTypes<IReadOnlyPositionBehavior>()
                        .Single()
                        .X);
                Assert.Equal(
                    -28.75,
                    wallBehaviors
                        .TakeTypes<IReadOnlyPositionBehavior>()
                        .Single()
                        .Y);
                Assert.Equal(
                    6,
                    wallBehaviors
                        .TakeTypes<IReadOnlySizeBehavior>()
                        .Single()
                        .Width);
                Assert.Equal(
                    1,
                    wallBehaviors
                        .TakeTypes<IReadOnlySizeBehavior>()
                        .Single()
                        .Height);
                Assert.Equal(
                    0,
                    wallBehaviors
                        .TakeTypes<IBoxColliderBehavior>()
                        .Single()
                        .X);
                Assert.Equal(
                    0,
                    wallBehaviors
                        .TakeTypes<IBoxColliderBehavior>()
                        .Single()
                        .Y);
                Assert.Equal(
                    1,
                    wallBehaviors
                        .TakeTypes<IBoxColliderBehavior>()
                        .Single()
                        .Width);
                Assert.Equal(
                    1,
                    wallBehaviors
                        .TakeTypes<IBoxColliderBehavior>()
                        .Single()
                        .Height);
                Assert.False(
                    wallBehaviors
                        .TakeTypes<IBoxColliderBehavior>()
                        .Single()
                        .IsTrigger,
                    "Expecting IsTrigger to be false.");
            });
        }

        [Fact]
        private void LoadForMap_TestEncounterMap_ContainerHasExpectedBehaviors()
        {
            _testAmenities.UsingCleanMapAndObjects(async () =>
            {
                var gameObjects = (await _mapGameObjectRepository
                    .LoadForMapAsync(new StringIdentifier("test_encounter_map")))
                    .ToArray();

                Assert.Equal(OBJ_COUNT_ON_TESTENCOUNTERMAP, gameObjects.Length);

                var containerBehaviors = gameObjects[7].Behaviors;

                Assert.Equal(12, containerBehaviors.Count);
                Assert.Equal(
                    new StringIdentifier("container"),
                    containerBehaviors
                        .TakeTypes<IReadOnlyTypeIdentifierBehavior>()
                        .Single()
                        .TypeId);
                Assert.Equal(
                    new StringIdentifier("container/base"),
                    containerBehaviors
                        .TakeTypes<IReadOnlyTemplateIdentifierBehavior>()
                        .Single()
                        .TemplateId);
                Assert.Equal(
                    new StringIdentifier("container/base"),
                    containerBehaviors
                        .TakeTypes<ICreatedFromTemplateBehavior>()
                        .Single()
                        .TemplateId);
                Assert.Equal(
                    new StringIdentifier("container--c6a7f8b9-000a-41db-ac5e-d321f1722bf8"),
                    containerBehaviors
                        .TakeTypes<IReadOnlyIdentifierBehavior>()
                        .Single()
                        .Id);
                Assert.Equal(
                    new StringIdentifier("Mapping/Prefabs/container/chest"),
                    containerBehaviors
                        .TakeTypes<IReadOnlyPrefabResourceIdBehavior>()
                        .Single()
                        .PrefabResourceId);
                Assert.Equal(
                    40,
                    containerBehaviors
                        .TakeTypes<IReadOnlyPositionBehavior>()
                        .Single()
                        .X);
                Assert.Equal(
                    -22.25,
                    containerBehaviors
                        .TakeTypes<IReadOnlyPositionBehavior>()
                        .Single()
                        .Y);
                Assert.Equal(
                    1,
                    containerBehaviors
                        .TakeTypes<IReadOnlySizeBehavior>()
                        .Single()
                        .Width);
                Assert.Equal(
                    1,
                    containerBehaviors
                        .TakeTypes<IReadOnlySizeBehavior>()
                        .Single()
                        .Height);
                Assert.False(
                    containerBehaviors
                        .TakeTypes<IContainerInteractableBehavior>()
                        .Single()
                        .AutomaticInteraction,
                    "Expecting to not have automatic interaction on container itneractable behavior.");
                Assert.False(
                    containerBehaviors
                        .TakeTypes<IContainerInteractableBehavior>()
                        .Single()
                        .DestroyOnUse,
                    "Expecting to not have destroy on use on container properties behavior.");
                Assert.True(
                    containerBehaviors
                        .TakeTypes<IContainerInteractableBehavior>()
                        .Single()
                        .TransferItemsOnActivate,
                    "Expecting to have transfer items on activate on container properties behavior.");
                Assert.Equal(
                    new StringIdentifier("any_normal_magic_rare_10x_lvl10"),
                    containerBehaviors
                        .TakeTypes<IReadOnlyContainerGenerateItemsBehavior>()
                        .Single()
                        .DropTableId);
                Assert.False(
                    containerBehaviors
                        .TakeTypes<IReadOnlyContainerGenerateItemsBehavior>()
                        .Single()
                        .HasGeneratedItems,
                    "Expecting to not have generated items on use on container generate items behavior.");
                Assert.Single(containerBehaviors.TakeTypes<IMakeNoiseBehaviour>());
                Assert.Equal(
                    0,
                    containerBehaviors
                        .TakeTypes<IBoxColliderBehavior>()
                        .Single()
                        .X);
                Assert.Equal(
                    0,
                    containerBehaviors
                        .TakeTypes<IBoxColliderBehavior>()
                        .Single()
                        .Y);
                Assert.Equal(
                    1,
                    containerBehaviors
                        .TakeTypes<IBoxColliderBehavior>()
                        .Single()
                        .Width);
                Assert.Equal(
                    1,
                    containerBehaviors
                        .TakeTypes<IBoxColliderBehavior>()
                        .Single()
                        .Height);
                Assert.False(
                    containerBehaviors
                        .TakeTypes<IBoxColliderBehavior>()
                        .Single()
                        .IsTrigger,
                    "Expecting IsTrigger to be false.");
                Assert.Equal(
                    new StringIdentifier("Items"),
                    containerBehaviors
                        .TakeTypes<IReadOnlyItemContainerBehavior>()
                        .Single()
                        .ContainerId);
                Assert.Empty(
                    containerBehaviors
                        .TakeTypes<IReadOnlyItemContainerBehavior>()
                        .Single()
                        .Items);
            });
        }

        [Fact]
        private void LoadForMap_TestEncounterMap_TriggerOnCombatEndDoorExpectedBehaviors()
        {
            _testAmenities.UsingCleanMapAndObjects(async () =>
            {
                var gameObjects = (await _mapGameObjectRepository
                    .LoadForMapAsync(new StringIdentifier("test_encounter_map")))
                    .ToArray();

                Assert.Equal(OBJ_COUNT_ON_TESTENCOUNTERMAP, gameObjects.Length);

                var targetBehaviors = gameObjects[13].Behaviors;

                Assert.Equal(7, targetBehaviors.Count);
                
                var spawnTemplatePropertiesBehavior = Assert.Single(targetBehaviors.TakeTypes<IReadOnlySpawnTemplatePropertiesBehavior>());
                Assert.NotNull(spawnTemplatePropertiesBehavior.TemplateToSpawn);

                targetBehaviors = spawnTemplatePropertiesBehavior.TemplateToSpawn.Behaviors;
                Assert.Equal(6, targetBehaviors.Count);
                Assert.Equal(
                    new StringIdentifier("static"),
                    targetBehaviors
                        .TakeTypes<IReadOnlyTypeIdentifierBehavior>()
                        .Single()
                        .TypeId);
                Assert.Empty(targetBehaviors.TakeTypes<IReadOnlyTemplateIdentifierBehavior>());
                Assert.Equal(
                    new StringIdentifier("Mapping/Prefabs/static/encounterspawn"),
                    targetBehaviors
                        .TakeTypes<IReadOnlyPrefabResourceIdBehavior>()
                        .Single()
                        .PrefabResourceId);
                // FIXME: does this need to have an identifier behavior or can we add one on the fly?
                Assert.Equal(
                    new StringIdentifier("XXX - Does this need to be here or can we autogenerate it?"),
                    targetBehaviors
                        .TakeTypes<IReadOnlyIdentifierBehavior>()
                        .Single()
                        .Id);
                Assert.Single(targetBehaviors.TakeTypes<IReadOnlyPositionBehavior>());
                Assert.Single(targetBehaviors.TakeTypes<IReadOnlySizeBehavior>());

                var doorBehavior = Assert.Single(targetBehaviors.TakeTypes<DoorInteractableBehavior>());
                Assert.Equal(
                    new StringIdentifier("swamp"),
                    doorBehavior.TransitionToMapId);
                Assert.False(
                    doorBehavior.AutomaticInteraction,
                    "Not expecting automatic interaction.");
                Assert.Equal(
                    40.0,
                    doorBehavior.TransitionToX);
                Assert.Equal(
                    -16.0,
                    doorBehavior.TransitionToY);
            });
        }
    }
}
