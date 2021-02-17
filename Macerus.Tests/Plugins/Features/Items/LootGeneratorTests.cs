using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Content.Wip.Items;
using Macerus.Plugins.Features.GameObjects.Items.Behaviors;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables;
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
        }

        private static IReadOnlyDictionary<string, IItemDefinition> AllowNormalItems =>
            _lazyAllowNormalItems.Value;

        private static IReadOnlyDictionary<string, IItemDefinition> AllowMagicItems =>
            _lazyAllowMagicItems.Value;

        [Fact]
        public void GenerateLootStressTest_NormalAndMagicTable_ItemsMeetAffixRequirements()
        {
            var lootGenerator = _container.Resolve<ILootGenerator>();
            var generatorContextProvider = _container.Resolve<IGeneratorContextProvider>();
            var generatorContext = generatorContextProvider.GetGeneratorContext();
            generatorContext = new GeneratorContext(
                10000,
                10000,
                generatorContext
                    .Attributes
                    .AppendSingle(new GeneratorAttribute(
                        new StringIdentifier("drop-table"),
                        new IdentifierGeneratorAttributeValue(new StringIdentifier("any_normal_magic_10x_lvl10")),
                        true)));

            foreach (var item in lootGenerator.GenerateLoot(generatorContext))
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
    }
}
