using System.Linq;

using Macerus.Plugins.Features.GameObjects.Items;
using Macerus.Plugins.Features.GameObjects.Items.Behaviors;
using Macerus.Plugins.Features.GameObjects.Items.Generation;
using Macerus.Plugins.Features.GameObjects.Items.Generation.Rare;

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
    public sealed class RareItemGenerationTests
    {
        private static readonly MacerusContainer _container;
        private static readonly AssertionHelpers _assertionHelpers;

        static RareItemGenerationTests()
        {
            _container = new MacerusContainer();
            _assertionHelpers = new AssertionHelpers(_container);
        }

        [Fact]
        public void GenerateRareItems_StressTest()
        {
            var itemGenerator = _container.Resolve<IItemGeneratorFacade>();
            var itemDefinitionRepository = _container.Resolve<IItemDefinitionRepositoryFacade>();

            var filterContextFactory = _container.Resolve<IFilterContextFactory>();
            var itemGenerationContext = filterContextFactory.CreateContext(
                10000,
                10000,
                new FilterAttribute(
                    new StringIdentifier("affix-type"),
                    new StringFilterAttributeValue("rare"),
                    true),
                new FilterAttribute(
                    new StringIdentifier("item-level"),
                    new DoubleFilterAttributeValue(5),
                    false));

            var generatedItems = itemGenerator
                .GenerateItems(itemGenerationContext)
                .ToArray();
            foreach (var item in generatedItems.Cast<IGameObject>())
            {
                _assertionHelpers.AssertAffix(
                    item,
                    new StringIdentifier("rare"));

                Assert.InRange(
                    item.GetOnly<IHasEnchantmentsBehavior>().Enchantments.Count,
                    3,
                    6);
                // TODO: check that the enchantments are allowed to exist with each other
                // i.e. generating two enchantments that add + life when it might not be allowed

                var inventoryDisplayNames = item
                    .Get<IHasInventoryDisplayName>()
                    .ToArray();
                Assert.True(
                    2 == inventoryDisplayNames.Length,
                    $"Expecting to have two '{typeof(IHasInventoryDisplayName)}' (one for base name, one for rare name).");
                
                var baseItemInventoryDisplayName = (IBaseItemInventoryDisplayName)inventoryDisplayNames[0];
                Assert.True(
                    baseItemInventoryDisplayName.BaseItemStringResourceId != null,
                    $"Expecting '{baseItemInventoryDisplayName}' (base name) to have a populated string resource ID.");
                Assert.True(
                    !string.IsNullOrWhiteSpace(baseItemInventoryDisplayName.DisplayName),
                    $"Expecting '{baseItemInventoryDisplayName}' (base name) to have a populated display name.");

                var rareInventoryDisplayName = (IHasRareInventoryDisplayName)inventoryDisplayNames[1];
                Assert.True(
                    !string.IsNullOrWhiteSpace(rareInventoryDisplayName.DisplayName),
                    $"Expecting '{rareInventoryDisplayName}' (rare name) to have a populated display name.");
                Assert.True(
                    rareInventoryDisplayName.PrefixStringResourceId != null,
                    $"Expecting '{rareInventoryDisplayName}' (rare name) to have a populated prefix resource ID.");
                Assert.True(
                    rareInventoryDisplayName.SuffixStringResourceId != null,
                    $"Expecting '{rareInventoryDisplayName}' (rare name) to have a populated suffix resource ID.");

                _assertionHelpers.AssertSocketBehaviors(item);
            }
        }
    }
}
