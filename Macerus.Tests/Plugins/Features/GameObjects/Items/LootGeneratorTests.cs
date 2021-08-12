using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Autofac;

using Macerus.Plugins.Features.GameObjects.Items;
using Macerus.Plugins.Features.GameObjects.Items.Behaviors;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Default;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables.Standard;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.InMemory.DropTables;
using ProjectXyz.Shared.Framework;

using Xunit;
using Xunit.Sdk;

namespace Macerus.Tests.Plugins.Features.GameObjects.Items
{
    public sealed class LootGeneratorTests
    {
        private static readonly MacerusContainer _container;
        private static readonly TestAmenities _testAmenities;
        private static readonly Lazy<IReadOnlyDictionary<string, IItemDefinition>> _lazyAllowNormalItems;
        private static readonly Lazy<IReadOnlyDictionary<string, IItemDefinition>> _lazyAllowMagicItems;
        private static readonly Lazy<IReadOnlyDictionary<string, IItemDefinition>> _lazyAllowRareItems;
        private static readonly ILootGenerator _lootGenerator;
        private static readonly IFilterContextProvider _filterContextProvider;

        static LootGeneratorTests()
        {
            _container = new MacerusContainer();
            _testAmenities = new TestAmenities(_container);

            var filterContextFactory = _container.Resolve<IFilterContextFactory>();
            var itemDefinitionRepository = _container.Resolve<IItemDefinitionRepositoryFacade>();
            _lazyAllowNormalItems = new Lazy<IReadOnlyDictionary<string, IItemDefinition>>(() =>
                GetAllowedItemsByName(
                    filterContextFactory,
                    itemDefinitionRepository,
                    "normal"));
            _lazyAllowMagicItems = new Lazy<IReadOnlyDictionary<string, IItemDefinition>>(() =>
                GetAllowedItemsByName(
                    filterContextFactory,
                    itemDefinitionRepository,
                    "magic"));
            _lazyAllowRareItems = new Lazy<IReadOnlyDictionary<string, IItemDefinition>>(() =>
                GetAllowedItemsByName(
                    filterContextFactory,
                    itemDefinitionRepository,
                    "rare"));

            _lootGenerator = _container.Resolve<ILootGenerator>();
            _filterContextProvider = _container.Resolve<IFilterContextProvider>();
        }

        private static IReadOnlyDictionary<string, IItemDefinition> GetAllowedItemsByName(
            IFilterContextFactory filterContextFactory,
            IItemDefinitionRepositoryFacade itemDefinitionRepository,
            string affixType)
        {
            var allowedItems = itemDefinitionRepository
                .LoadItemDefinitions(filterContextFactory.CreateContext(
                    0,
                    int.MaxValue,
                    new FilterAttribute(
                        new StringIdentifier("affix-type"),
                        new StringFilterAttributeValue(affixType),
                        true)))
                .ToDictionary(
                    x => ((NameGeneratorComponent)x.GeneratorComponents.Single(c => c is NameGeneratorComponent)).DisplayName,
                    x => x);
            return allowedItems;
        }

        private static IReadOnlyDictionary<string, IItemDefinition> AllowNormalItems =>
            _lazyAllowNormalItems.Value;

        private static IReadOnlyDictionary<string, IItemDefinition> AllowMagicItems =>
            _lazyAllowMagicItems.Value;

        private static IReadOnlyDictionary<string, IItemDefinition> AllowRareItems =>
            _lazyAllowRareItems.Value;

        [Fact]
        public void GenerateLootStressTest_NormalMagicAndRareTable_ItemsMeetAffixRequirements()
        {
            var filterContext = _filterContextProvider.GetContext();
            filterContext = new FilterContext(
                10000,
                10000,
                filterContext
                    .Attributes
                    .AppendSingle(new FilterAttribute(
                        new StringIdentifier("drop-table"),
                        new IdentifierFilterAttributeValue(new StringIdentifier("any_normal_magic_rare_10x_lvl10")),
                        true)));

            foreach (var item in _lootGenerator.GenerateLoot(filterContext))
            {
                var affixTypeId = item
                   .GetOnly<IHasAffixType>()
                   .AffixTypeId;
                var baseDisplayName = item
                    .Get<IHasInventoryDisplayName>()
                    .First()
                    .DisplayName;

                if (affixTypeId.Equals(new StringIdentifier("normal")))
                {
                    Assert.True(
                        AllowNormalItems.ContainsKey(baseDisplayName),
                        $"Item was generated with affix type '{affixTypeId}' " +
                        $"but was not found in the allowed collection.");
                } 
                else if (affixTypeId.Equals(new StringIdentifier("magic")))
                {
                    Assert.True(
                        AllowMagicItems.ContainsKey(baseDisplayName),
                        $"Item was generated with affix type '{affixTypeId}' " +
                        $"but was not found in the allowed collection.");
                }
                else if (affixTypeId.Equals(new StringIdentifier("rare")))
                {
                    Assert.True(
                        AllowRareItems.ContainsKey(baseDisplayName),
                        $"Item was generated with affix type '{affixTypeId}' " +
                        $"but was not found in the allowed collection.");
                }
                else
                {
                    throw new XunitException(
                        $"Unhandled affix type id '{affixTypeId}'.");
                }
            }
        }

        [Fact]
        public async Task GenerateLoot_PlayerStatsRequiredPlayerPresentStatsMet_ExpectedDropTable()
        {
            await _testAmenities.UsingCleanMapAndObjectsWithPlayerAsync(async player =>
            {
                await player
                    .GetOnly<IHasStatsBehavior>()
                    .MutateStatsAsync(async stats => stats[new IntIdentifier(1)] = 15)
                    .ConfigureAwait(false);

                var context = _filterContextProvider
                    .GetContext()
                    .WithRange(1, 1)
                    .WithAdditionalAttributes(new[]
                    {
                        new FilterAttribute(
                            new StringIdentifier("drop-table"),
                            new IdentifierFilterAttributeValue(new StringIdentifier("GenerateLoot_PlayerStatsRequiredPlayerPresentStatsMet_ExpectedDropTable")),
                            true),
                    });
                var results = _lootGenerator
                    .GenerateLoot(context)
                    .ToArray();
                Assert.Single(results);
            });
        }

        [Fact]
        public async Task GenerateLoot_PlayerStatsRequiredPlayerPresentStatsNotMet_ThrowsNoValidTable()
        {
            await _testAmenities.UsingCleanMapAndObjectsWithPlayerAsync(async player =>
            {
                await player
                    .GetOnly<IHasStatsBehavior>()
                    .MutateStatsAsync(async stats => stats[new IntIdentifier(1)] = 5)
                    .ConfigureAwait(false);

                var context = _filterContextProvider
                    .GetContext()
                    .WithRange(1, 1)
                    .WithAdditionalAttributes(new[]
                    {
                        new FilterAttribute(
                            new StringIdentifier("drop-table"),
                            new IdentifierFilterAttributeValue(new StringIdentifier("GenerateLoot_PlayerStatsRequiredPlayerPresentStatsMet_ExpectedDropTable")),
                            true),
                    });

                var exception = Assert.Throws<InvalidOperationException>(() => _lootGenerator
                    .GenerateLoot(context)
                    .ToArray());
            });
        }

        public sealed class TestModule : SingleRegistrationModule
        {
            protected override void SafeLoad(ContainerBuilder builder)
            {
                builder
                    .Register(x =>
                    {
                        var dropTableFactory = x.Resolve<IItemDropTableFactory>();
                        var dropTables = new IDropTable[]
                        {
                            dropTableFactory.Create(
                                new StringIdentifier("GenerateLoot_PlayerStatsRequiredPlayerPresentStatsMet_ExpectedDropTable"),
                                3,
                                3,
                                new[]
                                {
                                    new FilterAttribute(
                                    new StringIdentifier("actor-stats"),
                                    new ActorStatFilterAttributeValue(
                                        new StringIdentifier("player"),
                                        new IntIdentifier(1),
                                        10,
                                        20),
                                    true),
                                },
                                new[]
                                {
                                    new FilterAttribute(
                                        new StringIdentifier("affix-type"),
                                        new StringFilterAttributeValue("magic"),
                                        true),
                                    new FilterAttribute(
                                        new StringIdentifier("item-level"),
                                        new DoubleFilterAttributeValue(0),
                                        false),
                                }),
                        };
                        return new InMemoryDropTableRepository(dropTables);
                    })
                    .AsImplementedInterfaces()
                    .SingleInstance();
            }
        }
    }
}
