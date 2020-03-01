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
                    false));
            var generatedItems = itemGenerator
                .GenerateItems(itemGenerationContext)
                .ToArray();
            foreach (var item in generatedItems.Cast<IHasBehaviors>())
            {
                Assert.InRange(
                    item.GetOnly<IHasEnchantmentsBehavior>().Enchantments.Count,
                    1,
                    2);
                Assert.True(
                    item.Has<IHasInventoryDisplayName>(),
                    $"Expecting to have behavior '{typeof(IHasInventoryDisplayName)}'.");
                Assert.True(
                    !string.IsNullOrWhiteSpace(item.GetOnly<IHasInventoryDisplayName>().DisplayName),
                    $"Expecting '{nameof(IHasInventoryDisplayName.DisplayName)}' to be populated.");
            }
        }
    }
}
