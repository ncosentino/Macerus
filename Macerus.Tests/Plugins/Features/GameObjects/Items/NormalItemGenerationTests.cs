﻿using System.Linq;

using Macerus.Plugins.Features.GameObjects.Items.Behaviors;
using Macerus.Plugins.Features.Resources.Default;

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
    public sealed class NormalItemGenerationTests
    {
        private static readonly MacerusContainer _container;
        private static readonly AssertionHelpers _assertionHelpers;

        static NormalItemGenerationTests()
        {
            _container = new MacerusContainer();
            _assertionHelpers = new AssertionHelpers(_container);
        }

        [Fact]
        public void GenerateNormalItems_StressTest()
        {
            var itemGenerator = _container.Resolve<IItemGeneratorFacade>();
            var imageResourceRepository = _container.Resolve<IImageResourceRepositoryFacade>();

            var filterContextFactory = _container.Resolve<IFilterContextFactory>();
            var itemGenerationContext = filterContextFactory.CreateContext(
                100000,
                100000,
                new FilterAttribute(
                    new StringIdentifier("affix-type"),
                    new StringFilterAttributeValue("normal"),
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

                _assertionHelpers.AssertSocketBehaviors(item);

                var inventoryIcons = item
                    .Get<IHasInventoryIcon>()
                    .ToArray();
                Assert.True(
                    1 == inventoryIcons.Length,
                    $"Expecting to have single '{typeof(IHasInventoryIcon)}'.");
                using (var resourceStream = imageResourceRepository.OpenStreamForResource(inventoryIcons.Single().IconResourceId))
                {
                    Assert.True(
                        resourceStream != null,
                        $"Could not find resource for '{inventoryIcons.Single().IconResourceId}'.");
                }
            }
        }        
    }
}
