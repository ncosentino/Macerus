using System;
using System.Collections.Generic;
using System.Globalization;

using Macerus.Api.GameObjects;
using Macerus.Plugins.Features.GameObjects.Static.Api;
using Macerus.Shared.Behaviors;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Static
{
    public sealed class StaticGameObjectRepository : IDiscoverableGameObjectRepository
    {
        private static readonly IIdentifier STATIC_TYPE_ID = new StringIdentifier("static");
        
        private readonly IStaticGameObjectFactory _staticGameObjectFactory;

        public StaticGameObjectRepository(IStaticGameObjectFactory staticGameObjectFactory)
        {
            _staticGameObjectFactory = staticGameObjectFactory;
        }

        public bool CanCreateFromTemplate(
            IIdentifier typeId,
            IIdentifier templateId)
        {
            var canCreateFromTemplate = typeId.Equals(STATIC_TYPE_ID) && templateId is StringIdentifier;
            return canCreateFromTemplate;
        }

        public bool CanLoad(
            IIdentifier typeId,
            IIdentifier objectId) => false;

        public IGameObject CreateFromTemplate(
            IIdentifier typeId,
            IIdentifier templateId,
            IReadOnlyDictionary<string, object> properties)
        {
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
                });
            return staticGameObject;
        }

        public IGameObject Load(
            IIdentifier typeId,
            IIdentifier objectId)
        {
            throw new NotSupportedException(
                $"'{GetType()}' does not support '{nameof(Load)}'.");
        }
    }
}
