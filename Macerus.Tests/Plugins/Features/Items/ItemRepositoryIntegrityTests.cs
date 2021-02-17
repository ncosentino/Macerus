using System.Linq;

using Macerus.Plugins.Content.Wip.Items;
using Macerus.Plugins.Features.GameObjects.Items.Affixes.Api;

using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;

using Xunit;

namespace Macerus.Tests.Plugins.Features.Items
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
            var generatorContextFactory = _container.Resolve<IGeneratorContextFactory>();
            var affixTypeRepository = _container.Resolve<IAffixTypeRepository>();

            var allAffixes = affixTypeRepository
                .GetAllAffixTypes()
                .Select(x => x.Name) // FIXME: this should be by id
                .ToArray();
            var allItems = itemDefinitionRepository
                .LoadItemDefinitions(generatorContextFactory.CreateGeneratorContext(
                    0,
                    int.MaxValue,
                    new GeneratorAttribute(
                        new StringIdentifier("affix-type"),
                        new AnyStringCollectionGeneratorAttributeValue(allAffixes),
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
