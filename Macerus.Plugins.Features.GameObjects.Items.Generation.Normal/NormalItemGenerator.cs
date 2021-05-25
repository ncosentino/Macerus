using System.Collections.Generic;
using System.Linq;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Plugins.Features.GameObjects.Items.Behaviors;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes; // FIXME: dependency on non-API
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Normal
{
    public sealed class NormalItemGenerator : IDiscoverableItemGenerator
    {
        private readonly IBaseItemGenerator _baseItemGenerator;
        private readonly IFilterContextAmenity _filterContextAmenity;
        private readonly IGameObjectFactory _gameObjectFactory;

        public NormalItemGenerator(
            IBaseItemGenerator baseItemGenerator,
            IFilterContextAmenity filterContextAmenity,
            IGameObjectFactory gameObjectFactory)
        {
            _baseItemGenerator = baseItemGenerator;
            _filterContextAmenity = filterContextAmenity;
            _gameObjectFactory = gameObjectFactory;
        }

        public IEnumerable<IGameObject> GenerateItems(IFilterContext filterContext)
        {
            var normalGeneratorContext = _filterContextAmenity.CreateSubContext(
                filterContext,
                SupportedAttributes);
            var baseItems = _baseItemGenerator.GenerateItems(normalGeneratorContext);
            var items = baseItems.Select(baseItem => _gameObjectFactory.Create(baseItem.Behaviors.Concat(new IBehavior[]
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
