using System;
using System.Collections.Generic;
using System.Linq;

using Autofac;

using Macerus.Plugins.Content.Wip.Items;
using Macerus.Plugins.Features.GameObjects.Items.Behaviors;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Api.Stats;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables.Implementations.Item;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.InMemory.DropTables;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.GameObjects.Generation;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;

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
        private static readonly IGeneratorContextProvider _generatorContextProvider;
        private static readonly IMutableGameObjectManager _gameObjectManager;
        private static readonly IActorFactory _actorFactory;

        static LootGeneratorTests()
        {
            _container = new MacerusContainer();
            _lazyAllowNormalItems = new Lazy<Dictionary<string, IItemDefinition>>(() =>
            {
                var generatorContextFactory = _container.Resolve<IGeneratorContextFactory>();
                var itemDefinitionRepository = _container.Resolve<IItemDefinitionRepositoryFacade>();
                var normalitems = itemDefinitionRepository
                    .LoadItemDefinitions(generatorContextFactory.CreateGeneratorContext(
                        0,
                        int.MaxValue,
                        new GeneratorAttribute(
                            new StringIdentifier("affix-type"),
                            new StringGeneratorAttributeValue("normal"),
                            true)))
                    .ToDictionary(
                        x => ((NameGeneratorComponent)x.GeneratorComponents.Single(c => c is NameGeneratorComponent)).DisplayName,
                        x => x);
                return normalitems;
            });
            _lazyAllowMagicItems = new Lazy<Dictionary<string, IItemDefinition>>(() =>
            {
                var generatorContextFactory = _container.Resolve<IGeneratorContextFactory>();
                var itemDefinitionRepository = _container.Resolve<IItemDefinitionRepositoryFacade>();
                var allowMagicItems = itemDefinitionRepository
                    .LoadItemDefinitions(generatorContextFactory.CreateGeneratorContext(
                        0,
                        int.MaxValue,
                        new GeneratorAttribute(
                            new StringIdentifier("affix-type"),
                            new StringGeneratorAttributeValue("magic"),
                            true)))
                    .ToDictionary(
                        x => ((NameGeneratorComponent)x.GeneratorComponents.Single(c => c is NameGeneratorComponent)).DisplayName,
                        x => x);
                return allowMagicItems;
            });

            _lootGenerator = _container.Resolve<ILootGenerator>();
            _generatorContextProvider = _container.Resolve<IGeneratorContextProvider>();
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
            var generatorContext = _generatorContextProvider.GetGeneratorContext();
            generatorContext = new GeneratorContext(
                10000,
                10000,
                generatorContext
                    .Attributes
                    .AppendSingle(new GeneratorAttribute(
                        new StringIdentifier("drop-table"),
                        new IdentifierGeneratorAttributeValue(new StringIdentifier("any_normal_magic_10x_lvl10")),
                        true)));

            foreach (var item in _lootGenerator.GenerateLoot(generatorContext))
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

                var context = _generatorContextProvider
                    .GetGeneratorContext()
                    .WithGenerateCountRange(1, 1)
                    .WithAdditionalAttributes(new[]
                    {
                        new GeneratorAttribute(
                            new StringIdentifier("drop-table"),
                            new IdentifierGeneratorAttributeValue(new StringIdentifier("GenerateLoot_PlayerStatsRequiredPlayerPresentStatsMet_ExpectedDropTable")),
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

                var context = _generatorContextProvider
                    .GetGeneratorContext()
                    .WithGenerateCountRange(1, 1)
                    .WithAdditionalAttributes(new[]
                    {
                        new GeneratorAttribute(
                            new StringIdentifier("drop-table"),
                            new IdentifierGeneratorAttributeValue(new StringIdentifier("GenerateLoot_PlayerStatsRequiredPlayerPresentStatsMet_ExpectedDropTable")),
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
                            new GeneratorAttribute(
                            new StringIdentifier("actor-stats"),
                            new ActorStatGeneratorAttributeValue(
                                new StringIdentifier("player"),
                                new IntIdentifier(1),
                                10,
                                20),
                            true),
                        },
                        new[]
                        {
                            new GeneratorAttribute(
                                new StringIdentifier("affix-type"),
                                new StringGeneratorAttributeValue("magic"),
                                true),
                            new GeneratorAttribute(
                                new StringIdentifier("item-level"),
                                new DoubleGeneratorAttributeValue(0),
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
