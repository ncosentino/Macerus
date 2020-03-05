using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macerus.Plugins.Features.GameObjects.Items.Behaviors;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;
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

            var itemGenerationContextFactory = _container.Resolve<IGeneratorContextFactory>();
            var itemGenerationContext = itemGenerationContextFactory.CreateGeneratorContext(
                10000,
                10000,
                new GeneratorAttribute(
                    new StringIdentifier("affix-type"),
                    new StringGeneratorAttributeValue("magic"),
                    true),
                new GeneratorAttribute(
                    new StringIdentifier("item-level"),
                    new DoubleGeneratorAttributeValue(5),
                    true));
            var generatedItems = itemGenerator
                .GenerateItems(itemGenerationContext)
                .ToArray();
            foreach (var item in generatedItems.Cast<IHasBehaviors>())
            {
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
            }
        }
    }
}
