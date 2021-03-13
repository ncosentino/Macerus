using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Macerus.Api.Behaviors;
using Macerus.Api.Behaviors.Filtering;
using Macerus.Api.GameObjects;
using Macerus.Shared.Behaviors;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Skills;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Actors
{
    public sealed class ActorRepository : IDiscoverableGameObjectRepository
    {
        private static readonly IIdentifier PLAYER_TEMPLATE_ID = new StringIdentifier("player");

        private readonly IActorFactory _actorFactory;
        private readonly IActorIdentifiers _actorIdentifiers;
        private readonly IAttributeFilterer _attributeFilterer;
        private readonly IFilterContextAmenity _filterContextAmenity;

        public ActorRepository(
            IActorFactory actorFactory,
            IActorIdentifiers actorIdentifiers,
            IFilterContextAmenity filterContextAmenity,
            IAttributeFilterer attributeFilterer)
        {
            _actorFactory = actorFactory;
            _actorIdentifiers = actorIdentifiers;
            _filterContextAmenity = filterContextAmenity;
            _attributeFilterer = attributeFilterer;
        }

        public IEnumerable<IGameObject> CreateFromTemplate(
            IFilterContext filterContext,
            IReadOnlyDictionary<string, object> properties)
        {
            // FIXME: actually extend this for templates and use an attribute filterer
            var typeId = _filterContextAmenity.GetGameObjectTypeIdFromContext(filterContext);
            if (!_actorIdentifiers.ActorTypeIdentifier.Equals(typeId))
            {
                yield break;
            }

            var templateId = _filterContextAmenity.GetGameObjectTemplateIdFromContext(filterContext);
            if (PLAYER_TEMPLATE_ID.Equals(templateId))
            {
                var player = CreatePlayer(
                    filterContext,
                    properties);
                yield return player;
            }
        }

        public IEnumerable<IGameObject> Load(IFilterContext filterContext)
        {
            // FIXME: implement this
            yield break;
        }

        private IGameObject CreatePlayer(
            IFilterContext filterContext,
            IReadOnlyDictionary<string, object> properties)
        {
            var actor = _actorFactory.Create(
                new TypeIdentifierBehavior()
                {
                    TypeId = _filterContextAmenity.GetGameObjectTypeIdFromContext(filterContext),
                },
                new TemplateIdentifierBehavior()
                {
                    TemplateId = _filterContextAmenity.GetGameObjectTemplateIdFromContext(filterContext),
                },
                new IdentifierBehavior()
                {
                    Id = new StringIdentifier("Player"),
                },
                new IBehavior[]
                {
                    new PlayerControlledBehavior(),
                    new ItemContainerBehavior(new StringIdentifier("Inventory")),
                    new ItemContainerBehavior(new StringIdentifier("Belt")),
                    new HasSkillsBehavior(),
                    new CanEquipBehavior(new[]
                    {
                        new StringIdentifier("head"),
                        new StringIdentifier("body"),
                        new StringIdentifier("left hand"),
                        new StringIdentifier("right hand"),
                        new StringIdentifier("amulet"),
                        new StringIdentifier("ring1"),
                        new StringIdentifier("ring2"),
                        new StringIdentifier("shoulders"),
                        new StringIdentifier("hands"),
                        new StringIdentifier("waist"),
                        new StringIdentifier("feet"),
                        new StringIdentifier("legs"),
                        new StringIdentifier("back"),
                    }),
                });

            var worldLocation = actor.Get<IWorldLocationBehavior>().Single();
            worldLocation.X = properties.TryGetValue("X", out var rawX)
                ? Convert.ToDouble(rawX, CultureInfo.InvariantCulture)
                : 0;
            worldLocation.Y = properties.TryGetValue("Y", out var rawY)
                ? Convert.ToDouble(rawY, CultureInfo.InvariantCulture)
                : 0;
            worldLocation.Width = properties.TryGetValue("Width", out var rawWidth)
                ? Convert.ToDouble(rawWidth, CultureInfo.InvariantCulture)
                : 1;
            worldLocation.Height = properties.TryGetValue("Height", out var rawHeight)
                ? Convert.ToDouble(rawHeight, CultureInfo.InvariantCulture)
                : 1;

            return actor;
        }
    }
}
