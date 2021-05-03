using System.Linq;

using Macerus.Plugins.Features.GameObjects.Items.Behaviors;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;
using ProjectXyz.Shared.Framework;

using Xunit;

namespace Macerus.Tests.Plugins.Features.GameObjects.Items
{
    public sealed class UniqueItemGenerationTests
    {
        private static readonly MacerusContainer _container;
        private static readonly AssertionHelpers _assertionHelpers;

        static UniqueItemGenerationTests()
        {
            _container = new MacerusContainer();
            _assertionHelpers = new AssertionHelpers(_container);
        }

        [Fact]
        public void GenerateUniqueItems_StressTest()
        {
            var itemGenerator = _container.Resolve<IItemGeneratorFacade>();
            var filterContextFactory = _container.Resolve<IFilterContextFactory>();

            var allUniqueItemIds = _container
                .Resolve<IItemDefinitionRepositoryFacade>()
                .LoadItemDefinitions(filterContextFactory.CreateContext(
                    0,
                    int.MaxValue,
                    new FilterAttribute(
                        new StringIdentifier("affix-type"),
                        new StringFilterAttributeValue("unique"),
                        true)))
                .Select(x => x
                .SupportedAttributes
                .SingleOrDefault(attr => attr.Id.Equals(new StringIdentifier("item-id")))
                .Value)
                .Select(x => ((IdentifierFilterAttributeValue)x).Value)
                .ToHashSet();

            foreach (var uniqueItemId in allUniqueItemIds)
            {
                var itemGenerationContext = filterContextFactory.CreateContext(
                    1,
                    1,
                    new FilterAttribute(
                        new StringIdentifier("affix-type"),
                        new StringFilterAttributeValue("unique"),
                        true),
                    new FilterAttribute(
                        new StringIdentifier("item-id"),
                        new IdentifierFilterAttributeValue(uniqueItemId),
                        false),
                    new FilterAttribute(
                        new StringIdentifier("item-level"),
                        new DoubleFilterAttributeValue(double.MaxValue),
                        false));

                var generatedItems = itemGenerator
                    .GenerateItems(itemGenerationContext)
                    .ToArray();
                Assert.Single(generatedItems);
                var item = generatedItems.Single();

                _assertionHelpers.AssertAffix(
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

                _assertionHelpers.AssertSocketBehaviors(item);
            }
        }        
    }
}
