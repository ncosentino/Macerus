using System.Linq;

using Macerus.Plugins.Features.GameObjects.Items;
using Macerus.Plugins.Features.GameObjects.Items.Affixes.Api;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation;
using ProjectXyz.Shared.Framework;

using Xunit;

namespace Macerus.Tests.Plugins.Features.GameObjects.Items
{
    public sealed class ItemRepositoryIntegrityTests
    {
        private static readonly MacerusContainer _container;

        static ItemRepositoryIntegrityTests()
        {
            _container = new MacerusContainer();
        }

        [Fact]
        public void AllItemDefinitions_QueriedByAffix_NoRequiredAttributeCollisions()
        {
            var itemDefinitionRepository = _container.Resolve<IItemDefinitionRepositoryFacade>();
            var filterContextFactory = _container.Resolve<IFilterContextFactory>();
            var affixTypeRepository = _container.Resolve<IAffixTypeRepository>();

            var allAffixes = affixTypeRepository
                .GetAllAffixTypes()
                .Select(x => x.Name) // FIXME: this should be by id
                .ToArray();
            var allItems = itemDefinitionRepository
                .LoadItemDefinitions(filterContextFactory.CreateContext(
                    0,
                    int.MaxValue,
                    new FilterAttribute(
                        new StringIdentifier("affix-type"),
                        new AnyStringCollectionFilterAttributeValue(allAffixes),
                        true)))
                .ToDictionary(
                    x => ((NameGeneratorComponent)x.GeneratorComponents.Single(c => c is NameGeneratorComponent)).DisplayName,
                    x => x);

            foreach (var entry in allItems)
            {
                var name = entry.Key;
                var item = entry.Value;
                
                foreach (var requiredAttribute in item
                    .SupportedAttributes
                    .Where(x => x.Required))
                {
                    var otherAttributes = item
                        .SupportedAttributes
                        .Where(x => !x.Required && x.Id.Equals(requiredAttribute.Id))
                        .ToArray();
                    Assert.Empty(otherAttributes);
                }
            }
        }
    }
}
