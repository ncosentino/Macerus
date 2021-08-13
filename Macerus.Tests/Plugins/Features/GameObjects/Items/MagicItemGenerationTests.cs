using System.Linq;

using Macerus.Plugins.Features.GameObjects.Items;
using Macerus.Plugins.Features.GameObjects.Items.Behaviors;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation;
using ProjectXyz.Shared.Framework;

using Xunit;

namespace Macerus.Tests.Plugins.Features.GameObjects.Items
{
    public sealed class MagicItemGenerationTests
    {
        private static readonly MacerusContainer _container;
        private static readonly AssertionHelpers _assertionHelpers;

        static MagicItemGenerationTests()
        {
            _container = new MacerusContainer();
            _assertionHelpers = new AssertionHelpers(_container);
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
                    x => ((NameGeneratorComponent)x.GeneratorComponents.Single(c => c is NameGeneratorComponent)).DisplayName,
                    x => x);

            var generatedItems = itemGenerator
                .GenerateItems(itemGenerationContext)
                .ToArray();
            foreach (var item in generatedItems.Cast<IGameObject>())
            {
                _assertionHelpers.AssertAffix(
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

                _assertionHelpers.AssertSocketBehaviors(item);
            }
        }
    }
}
