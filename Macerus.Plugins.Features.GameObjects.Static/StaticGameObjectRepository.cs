using System;
using System.Collections.Generic;
using System.Globalization;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Api.GameObjects;
using Macerus.Shared.Behaviors;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Static
{

    public sealed class StaticGameObjectRepository : IDiscoverableGameObjectRepository
    {
        private readonly IStaticGameObjectIdentifiers _staticGameObjectIdentifiers;
        private readonly IStaticGameObjectFactory _staticGameObjectFactory;
        private readonly IFilterContextAmenity _filterContextAmenity;
        private readonly IAttributeFilterer _attributeFilterer;

        public StaticGameObjectRepository(
            IStaticGameObjectIdentifiers staticGameObjectIdentifiers,
            IStaticGameObjectFactory staticGameObjectFactory,
            IFilterContextAmenity filterContextAmenity,
            IAttributeFilterer attributeFilterer)
        {
            _staticGameObjectIdentifiers = staticGameObjectIdentifiers;
            _staticGameObjectFactory = staticGameObjectFactory;
            _filterContextAmenity = filterContextAmenity;
            _attributeFilterer = attributeFilterer;
        }

        public IEnumerable<IGameObject> CreateFromTemplate(
            IFilterContext filterContext,
            IReadOnlyDictionary<string, object> properties)
        {
            // FIXME: actually extend this for templates and use an attribute filterer
            var typeId = _filterContextAmenity.GetGameObjectTypeIdFromContext(filterContext);
            if (!_staticGameObjectIdentifiers.StaticGameObjectTypeIdentifier.Equals(typeId))
            {
                yield break;
            }

            var templateId = _filterContextAmenity.GetGameObjectTemplateIdFromContext(filterContext);

            var staticGameObject = _staticGameObjectFactory.Create(
                new TypeIdentifierBehavior()
                {
                    TypeId = typeId
                },
                new TemplateIdentifierBehavior()
                {
                    TemplateId = templateId
                },
                new IdentifierBehavior()
                {
                    Id = new StringIdentifier($"{typeId}-{templateId}-{Guid.NewGuid()}"),
                },
                new WorldLocationBehavior()
                {
                    X = Convert.ToDouble(properties["X"], CultureInfo.InvariantCulture),
                    Y = Convert.ToDouble(properties["Y"], CultureInfo.InvariantCulture),
                    Width = Convert.ToDouble(properties["Width"], CultureInfo.InvariantCulture),
                    Height = Convert.ToDouble(properties["Height"], CultureInfo.InvariantCulture),
                },
                new StaticGameObjectPropertiesBehavior(properties));
            yield return staticGameObject;
        }

        public IEnumerable<IGameObject> Load(IFilterContext filterContext)
        {
            // FIXME: implement this
            yield break;
        }
    }
}
