using System.Linq;

using Macerus.Plugins.Features.GameObjects.Items.Behaviors;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Socketing.Api;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;

using Xunit;

namespace Macerus.Tests.Plugins.Features.Items
{
    public sealed class UniqueItemGenerationTests
    {
        private static readonly MacerusContainer _container;

        static UniqueItemGenerationTests()
        {
            _container = new MacerusContainer();
        }

        [Fact]
        public void GenerateUniqueItems_StressTest()
        {
            var itemGenerator = _container.Resolve<IItemGeneratorFacade>();
            var generatorContextFactory = _container.Resolve<IGeneratorContextFactory>();

            var allUniqueItemIds = _container
                .Resolve<IItemDefinitionRepositoryFacade>()
                .LoadItemDefinitions(generatorContextFactory.CreateGeneratorContext(
                    0,
                    int.MaxValue,
                    new GeneratorAttribute(
                        new StringIdentifier("affix-type"),
                        new StringGeneratorAttributeValue("unique"),
                        true)))
                .Select(x => x
                .SupportedAttributes
                .SingleOrDefault(attr => attr.Id.Equals(new StringIdentifier("item-id")))
                .Value)
                .Select(x => ((IdentifierGeneratorAttributeValue)x).Value)
                .ToHashSet();

            foreach (var uniqueItemId in allUniqueItemIds)
            {
                var itemGenerationContext = generatorContextFactory.CreateGeneratorContext(
                    1,
                    1,
                    new GeneratorAttribute(
                        new StringIdentifier("affix-type"),
                        new StringGeneratorAttributeValue("unique"),
                        true),
                    new GeneratorAttribute(
                        new StringIdentifier("item-id"),
                        new IdentifierGeneratorAttributeValue(uniqueItemId),
                        false),
                    new GeneratorAttribute(
                        new StringIdentifier("item-level"),
                        new DoubleGeneratorAttributeValue(double.MaxValue),
                        false));

                var generatedItems = itemGenerator
                    .GenerateItems(itemGenerationContext)
                    .ToArray();
                Assert.Single(generatedItems);
                var item = generatedItems.Single();

                AssertionHelpers.AssertAffix(
                    item,
                    new StringIdentifier("unique"));

                var enchantmentBehaviors = item
                    .Get<IHasEnchantmentsBehavior>()
                    .ToArray();
                Assert.True(
                    enchantmentBehaviors.Length == 1,
                    "We expect unique items have enchantments.");

                var inventoryDisplayNames = item
                    .Get<IHasInventoryDisplayName>()
                    .ToArray();
                Assert.True(
                    2 == inventoryDisplayNames.Length,
                    $"Expecting to have two '{typeof(IHasInventoryDisplayName)}' (one for base name, one for unique name).");
                Assert.True(
                    !string.IsNullOrWhiteSpace(inventoryDisplayNames[0].DisplayName),
                    $"Expecting '{inventoryDisplayNames[0]}' (base name) to have a populated display name.");
                Assert.True(
                    !string.IsNullOrWhiteSpace(inventoryDisplayNames[1].DisplayName),
                    $"Expecting '{inventoryDisplayNames[1]}' (unique name) to have a populated display name.");
                Assert.NotEqual(
                    inventoryDisplayNames[0].DisplayName,
                    inventoryDisplayNames[1].DisplayName);

                AssertionHelpers.AssertSocketBehaviors(item);
            }
        }        
    }
}
