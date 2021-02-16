using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Macerus.Api.Behaviors;
using Macerus.Api.GameObjects;
using Macerus.Shared.Behaviors;

using NexusLabs.Contracts;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Actors
{
    public sealed class ActorRepository : IDiscoverableGameObjectRepository
    {
        private static readonly IIdentifier ACTOR_TYPE_ID = new StringIdentifier("actor");
        private static readonly IIdentifier PLAYER_TEMPLATE_ID = new StringIdentifier("player");

        private readonly IActorFactory _actorFactory;

        public ActorRepository(IActorFactory actorFactory)
        {
            _actorFactory = actorFactory;
        }

        public static IIdentifier ActorTypeId => ACTOR_TYPE_ID;

        public bool CanCreateFromTemplate(
            IIdentifier typeId,
            IIdentifier templateId)
        {
            var canLoad = typeId.Equals(ACTOR_TYPE_ID) && templateId is StringIdentifier;
            return canLoad;
        }

        public bool CanLoad(IIdentifier typeId, IIdentifier objectId) => false; // FIXE: implement this

        public IGameObject CreateFromTemplate(
            IIdentifier typeId,
            IIdentifier templateId,
            IReadOnlyDictionary<string, object> properties)
        {
            Contract.Requires(
                ACTOR_TYPE_ID.Equals(typeId),
                $"Expecting to only support templates for type ID '{typeId}'.");

            if (PLAYER_TEMPLATE_ID.Equals(templateId))
            {
                return CreatePlayer(
                    typeId,
                    templateId,
                    properties);
            }

            throw new NotSupportedException(
                $"FIXME: currently no support for templates of type '{templateId}'.");
        }

        public IGameObject Load(IIdentifier typeId, IIdentifier objectId)
        {
            throw new NotImplementedException("FIXME: implement this");
        }

        private IGameObject CreatePlayer(
            IIdentifier typeId,
            IIdentifier templateId,
            IReadOnlyDictionary<string, object> properties)
        {
            var actor = _actorFactory.Create(
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
                    Id = new StringIdentifier("Player"),
                },
                new IBehavior[]
                {
                    new PlayerControlledBehavior(),
                    new ItemContainerBehavior(new StringIdentifier("Inventory")),
                    new ItemContainerBehavior(new StringIdentifier("Belt")),
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
            worldLocation.X = Convert.ToDouble(properties["X"], CultureInfo.InvariantCulture);
            worldLocation.Y = Convert.ToDouble(properties["Y"], CultureInfo.InvariantCulture);
            worldLocation.Width = Convert.ToDouble(properties["Width"], CultureInfo.InvariantCulture);
            worldLocation.Height = Convert.ToDouble(properties["Height"], CultureInfo.InvariantCulture);

            return actor;
        }
    }
}
