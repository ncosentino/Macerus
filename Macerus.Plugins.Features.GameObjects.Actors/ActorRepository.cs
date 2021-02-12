using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Macerus.Api.Behaviors;
using Macerus.Api.GameObjects;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Actors
{
    public sealed class ActorRepository : IDiscoverableGameObjectRepository
    {
        private static readonly IIdentifier ACTOR_TYPE_ID = new StringIdentifier("actor");

        private readonly IActorFactory _actorFactory;

        public ActorRepository(IActorFactory actorFactory)
        {
            _actorFactory = actorFactory;
        }

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
            var actor = _actorFactory.Create(
                new TypeIdentifierBehavior()
                {
                    TypeId = typeId
                },
                new TemplateIdentifierBehavior()
                {
                    TemplateId = typeId
                },
                // FIXME: this is just a hack to prove a point
                new IdentifierBehavior()
                {
                    Id = new StringIdentifier(properties["PlayerName"].ToString())
                });

            var location = actor.Get<IWorldLocationBehavior>().Single();
            location.X = Convert.ToDouble(properties["X"], CultureInfo.InvariantCulture);
            location.Y = Convert.ToDouble(properties["Y"], CultureInfo.InvariantCulture);

            return actor;
        }

        public IGameObject Load(IIdentifier typeId, IIdentifier objectId)
        {
            throw new NotImplementedException("FIXME: implement this");
        }
    }
}
