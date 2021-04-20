using System.Collections.Generic;
using System.Linq;

using Macerus.Api.Behaviors.Filtering;
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
        private readonly IFilterContextAmenity _filterContextAmenity;

        public NormalItemGenerator(
            IBaseItemGenerator baseItemGenerator,
            IFilterContextAmenity filterContextAmenity)
        {
            _baseItemGenerator = baseItemGenerator;
            _filterContextAmenity = filterContextAmenity;
        }

        public IEnumerable<IGameObject> GenerateItems(IFilterContext filterContext)
        {
            var normalGeneratorContext = _filterContextAmenity.CreateSubContext(
                filterContext,
                SupportedAttributes);
            var baseItems = _baseItemGenerator.GenerateItems(normalGeneratorContext);
            var items = baseItems.Select(baseItem => new NormalItem(baseItem.Behaviors.Concat(new IBehavior[]
            {
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
