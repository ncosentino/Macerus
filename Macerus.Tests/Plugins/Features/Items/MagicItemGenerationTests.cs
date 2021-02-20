using System.Linq;

using Macerus.Plugins.Content.Wip.Items;
using Macerus.Plugins.Features.GameObjects.Items.Behaviors;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;
using ProjectXyz.Shared.Behaviors.Filtering.Attributes;
using ProjectXyz.Shared.Framework;

using Xunit;

namespace Macerus.Tests.Plugins.Features.Items
{
    public sealed class MagicItemGenerationTests
    {
        private static readonly MacerusContainer _container;

        static MagicItemGenerationTests()
        {
            _container = new MacerusContainer();
        }

        [Fact]
        public void GenerateMagicItems_StressTest()
        {
            var itemGenerator = _container.Resolve<IItemGeneratorFacade>();
            var itemDefinitionRepository = _container.Resolve<IItemDefinitionRepositoryFacade>();

            var filterContextFactory = _container.Resolve<IFilterContextFactory>();
            var itemGenerationContext = filterContextFactory.CreateContext(
                10000,
                10000,
                new FilterAttribute(
                    new StringIdentifier("affix-type"),
                    new StringFilterAttributeValue("magic"),
                    true),
                new FilterAttribute(
                    new StringIdentifier("item-level"),
                    new DoubleFilterAttributeValue(5),
                    false));
            var nonMagicItems = itemDefinitionRepository
                .LoadItemDefinitions(filterContextFactory.CreateContext(
                    0,
                    int.MaxValue,
                    new FilterAttribute(
                        new StringIdentifier("affix-type"),
                        new NotFilterAttributeValue(new StringFilterAttributeValue("normal")),
                        true)))
                .ToDictionary(
                    x => ((NameFilterComponent)x.FilterComponents.Single(c => c is NameFilterComponent)).DisplayName,
                    x => x);

            var generatedItems = itemGenerator
                .GenerateItems(itemGenerationContext)
                .ToArray();
            foreach (var item in generatedItems.Cast<IHasBehaviors>())
            {
                AssertionHelpers.AssertAffix(
                    item,
                    new StringIdentifier("magic"));

                Assert.InRange(
                    item.GetOnly<IHasEnchantmentsBehavior>().Enchantments.Count,
                    1,
                    2);
                // TODO: check that the enchantments are allowed to exist with each other
                // i.e. generating two enchantments that add + life when it might not be allowed

                var inventoryDisplayNames = item
                    .Get<IHasInventoryDisplayName>()
                    .ToArray();
                Assert.True(
                    2 == inventoryDisplayNames.Length,
                    $"Expecting to have two '{typeof(IHasInventoryDisplayName)}' (one for base name, one for magic name).");
                Assert.True(
                    !string.IsNullOrWhiteSpace(inventoryDisplayNames[0].DisplayName),
                    $"Expecting '{inventoryDisplayNames[0]}' (base name) to have a populated display name.");
                Assert.True(
                    !string.IsNullOrWhiteSpace(inventoryDisplayNames[1].DisplayName),
                    $"Expecting '{inventoryDisplayNames[1]}' (magic name) to have a populated display name.");
                Assert.Contains(
                    inventoryDisplayNames[0].DisplayName,
                    inventoryDisplayNames[1].DisplayName);

                Assert.False(
                    nonMagicItems.ContainsKey(inventoryDisplayNames[0].DisplayName),
                    $"Expecting that '{inventoryDisplayNames[0].DisplayName}' (base name) cannot have magic affixes.");

                AssertionHelpers.AssertSocketBehaviors(item);
            }
        }
    }
}
