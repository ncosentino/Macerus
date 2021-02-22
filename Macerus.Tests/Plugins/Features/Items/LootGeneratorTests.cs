using System;
using System.Collections.Generic;
using System.Linq;

using Autofac;

using Macerus.Plugins.Features.GameObjects.Items;
using Macerus.Plugins.Features.GameObjects.Items.Behaviors;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables.Implementations.Item;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.InMemory.DropTables;
using ProjectXyz.Shared.Behaviors.Filtering;
using ProjectXyz.Shared.Behaviors.Filtering.Attributes;
using ProjectXyz.Shared.Framework;

using Xunit;
using Xunit.Sdk;

namespace Macerus.Tests.Plugins.Features.Items
{
    public sealed class LootGeneratorTests
    {
        private static readonly MacerusContainer _container;
        private static readonly Lazy<Dictionary<string, IItemDefinition>> _lazyAllowNormalItems;
        private static readonly Lazy<Dictionary<string, IItemDefinition>> _lazyAllowMagicItems;
        private static readonly ILootGenerator _lootGenerator;
        private static readonly IFilterContextProvider _filterContextProvider;
        private static readonly IMutableGameObjectManager _gameObjectManager;
        private static readonly IActorFactory _actorFactory;

        static LootGeneratorTests()
        {
            _container = new MacerusContainer();
            _lazyAllowNormalItems = new Lazy<Dictionary<string, IItemDefinition>>(() =>
            {
                var filterContextFactory = _container.Resolve<IFilterContextFactory>();
                var itemDefinitionRepository = _container.Resolve<IItemDefinitionRepositoryFacade>();
                var normalitems = itemDefinitionRepository
                    .LoadItemDefinitions(filterContextFactory.CreateContext(
                        0,
                        int.MaxValue,
                        new FilterAttribute(
                            new StringIdentifier("affix-type"),
                            new StringFilterAttributeValue("normal"),
                            true)))
                    .ToDictionary(
                        x => ((NameFilterComponent)x.FilterComponents.Single(c => c is NameFilterComponent)).DisplayName,
                        x => x);
                return normalitems;
            });
            _lazyAllowMagicItems = new Lazy<Dictionary<string, IItemDefinition>>(() =>
            {
                var filterContextFactory = _container.Resolve<IFilterContextFactory>();
                var itemDefinitionRepository = _container.Resolve<IItemDefinitionRepositoryFacade>();
                var allowMagicItems = itemDefinitionRepository
                    .LoadItemDefinitions(filterContextFactory.CreateContext(
                        0,
                        int.MaxValue,
                        new FilterAttribute(
                            new StringIdentifier("affix-type"),
                            new StringFilterAttributeValue("magic"),
                            true)))
                    .ToDictionary(
                        x => ((NameFilterComponent)x.FilterComponents.Single(c => c is NameFilterComponent)).DisplayName,
                        x => x);
                return allowMagicItems;
            });

            _lootGenerator = _container.Resolve<ILootGenerator>();
            _filterContextProvider = _container.Resolve<IFilterContextProvider>();
            _gameObjectManager = _container.Resolve<IMutableGameObjectManager>();
            _actorFactory = _container.Resolve<IActorFactory>();
        }

        private static IReadOnlyDictionary<string, IItemDefinition> AllowNormalItems =>
            _lazyAllowNormalItems.Value;

        private static IReadOnlyDictionary<string, IItemDefinition> AllowMagicItems =>
            _lazyAllowMagicItems.Value;

        [Fact]
        public void GenerateLootStressTest_NormalAndMagicTable_ItemsMeetAffixRequirements()
        {
            var filterContext = _filterContextProvider.GetContext();
            filterContext = new FilterContext(
                10000,
                10000,
                filterContext
                    .Attributes
                    .AppendSingle(new FilterAttribute(
                        new StringIdentifier("drop-table"),
                        new IdentifierFilterAttributeValue(new StringIdentifier("any_normal_magic_10x_lvl10")),
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
                else
                {
                    throw new XunitException(
                        $"Unhandled affix type id '{affixTypeId}'.");
                }
            }
        }

        [Fact]
        public void GenerateLoot_PlayerStatsRequiredPlayerPresentStatsMet_ExpectedDropTable()
        {
            UsingPlayer(player =>
            {
                player
                    .GetOnly<IHasMutableStatsBehavior>()
                    .MutateStats(stats => stats[new IntIdentifier(1)] = 15);

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
        public void GenerateLoot_PlayerStatsRequiredPlayerPresentStatsNotMet_ThrowsNoValidTable()
        {
            UsingPlayer(player =>
            {
                player
                    .GetOnly<IHasMutableStatsBehavior>()
                    .MutateStats(stats => stats[new IntIdentifier(1)] = 5);

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

        private void UsingPlayer(Action<IGameObject> callback)
        {
            var actor = _actorFactory.Create(
                new TypeIdentifierBehavior()
                {
                    TypeId = new StringIdentifier("actor")
                },
                new TemplateIdentifierBehavior()
                {
                    TemplateId = new StringIdentifier("player-template")
                },
                new IdentifierBehavior()
                {
                    Id = new StringIdentifier("player")
                },
                Enumerable.Empty<IBehavior>());

            _gameObjectManager.MarkForAddition(actor);
            try
            {
                _gameObjectManager.Synchronize();
                callback.Invoke(actor);
            }
            finally
            {
                _gameObjectManager.MarkForRemoval(actor);
                _gameObjectManager.Synchronize();
            }
        }

        public sealed class TestModule : SingleRegistrationModule
        {
            protected override void SafeLoad(ContainerBuilder builder)
            {
                var dropTables = new IDropTable[]
                {
                    new ItemDropTable(
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

                builder
                    .Register(x => new InMemoryDropTableRepository(dropTables))
                    .AsImplementedInterfaces()
                    .SingleInstance();
            }
        }
    }
}
