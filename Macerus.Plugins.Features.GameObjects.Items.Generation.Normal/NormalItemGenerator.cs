using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.GameObjects.Items.Behaviors;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes; // FIXME: dependency on non-API
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Normal
{
    public sealed class NormalItemGenerator : IDiscoverableItemGenerator
    {
        private readonly IBaseItemGenerator _baseItemGenerator;
        private readonly IFilterContextFactory _filterContextFactory;

        public NormalItemGenerator(
            IBaseItemGenerator baseItemGenerator,
            IFilterContextFactory filterContextFactory)
        {
            _baseItemGenerator = baseItemGenerator;
            _filterContextFactory = filterContextFactory;
        }

        public IEnumerable<IGameObject> GenerateItems(IFilterContext filterContext)
        {
            // create our new context by keeping information about attributes 
            // from our caller, but acknowledging that any that were required
            // are now fulfilled up until this point. we then cobine in the
            // newly provided attributes from the drop table.
            var generatorRequired = SupportedAttributes
                .Where(attr => attr.Required)
                .ToDictionary(x => x.Id, x => x);
            var normalGeneratorContext = _filterContextFactory.CreateContext(
                filterContext.MinimumCount,
                filterContext.MaximumCount,
                filterContext
                    .Attributes
                    .Where(x => !generatorRequired.ContainsKey(x.Id))
                    .Select(x => x.Required
                        ? x.CopyWithRequired(false)
                        : x)
                    .Concat(generatorRequired.Values));
            var baseItems = _baseItemGenerator.GenerateItems(normalGeneratorContext);
            var items = baseItems.Select(baseItem => new NormalItem(baseItem.Behaviors.Concat(new IBehavior[]
            {
                new HasInventoryDisplayColor(255, 255, 255, 255),
                new HasAffixType(new StringIdentifier("normal")),
            })));
            return items;
        }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; } = new IFilterAttribute[]
        {
            new FilterAttribute(
                new StringIdentifier("affix-type"),
                new StringFilterAttributeValue("normal"),
                true),
        };
    }
}
