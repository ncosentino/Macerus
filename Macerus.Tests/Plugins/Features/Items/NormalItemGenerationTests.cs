using System.Linq;

using Macerus.Plugins.Features.GameObjects.Items.Behaviors;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Socketing.Api;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;

using Xunit;

namespace Macerus.Tests.Plugins.Features.Items
{
    public sealed class NormalItemGenerationTests
    {
        private static readonly MacerusContainer _container;

        static NormalItemGenerationTests()
        {
            _container = new MacerusContainer();
        }

        [Fact]
        public void GenerateNormalItems_StressTest()
        {
            var itemGenerator = _container.Resolve<IItemGeneratorFacade>();

            var generatorContextFactory = _container.Resolve<IGeneratorContextFactory>();
            var itemGenerationContext = generatorContextFactory.CreateGeneratorContext(
                100000,
                100000,
                new GeneratorAttribute(
                    new StringIdentifier("affix-type"),
                    new StringGeneratorAttributeValue("normal"),
                    true),
                new GeneratorAttribute(
                    new StringIdentifier("item-level"),
                    new DoubleGeneratorAttributeValue(5),
                    false));

            var generatedItems = itemGenerator
                .GenerateItems(itemGenerationContext)
                .ToArray();
            foreach (var item in generatedItems.Cast<IHasBehaviors>())
            {
                AssertionHelpers.AssertAffix(
                    item,
                    new StringIdentifier("normal"));

                var enchantmentBehaviors = item
                    .Get<IHasEnchantmentsBehavior>()
                    .ToArray();
                Assert.True(
                    enchantmentBehaviors.Length < 1,
                    "For now, we expect normal items don't have enchantments. " +
                    "FIXME: when we can generate implicit modifiers on items " +
                    "that aren't magic affixes.");

                var inventoryDisplayNames = item
                    .Get<IHasInventoryDisplayName>()
                    .ToArray();
                Assert.True(
                    1 == inventoryDisplayNames.Length,
                    $"Expecting to have single '{typeof(IHasInventoryDisplayName)}'.");
                Assert.True(
                    !string.IsNullOrWhiteSpace(inventoryDisplayNames[0].DisplayName),
                    $"Expecting '{inventoryDisplayNames[0]}' to have a populated display name.");

                AssertionHelpers.AssertSocketBehaviors(item);
            }
        }        
    }
}
