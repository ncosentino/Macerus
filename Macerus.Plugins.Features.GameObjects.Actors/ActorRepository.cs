using System;
using System.Collections.Generic;
using System.Linq;
using Macerus.Api.Behaviors;
using Macerus.Api.GameObjects;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
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
            // TODO: actually load one up based on the ID instead of generating a new one :)
            var actor = _actorFactory.Create();

            var identifier = actor.Behaviors.Get<IIdentifierBehavior>().Single();
            // FIXME: this is just a hack to prove a point
            identifier.Id = new StringIdentifier(properties["PlayerName"].ToString());

            var location = actor.Behaviors.Get<IWorldLocationBehavior>().Single();
            location.X = 40;
            location.Y = -25;

            return actor;
        }

        public IGameObject Load(IIdentifier typeId, IIdentifier objectId)
        {
            throw new NotImplementedException("FIXME: implement this");
        }
    }
}
