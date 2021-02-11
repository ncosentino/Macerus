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

        public bool CanLoad(IIdentifier typeId, IIdentifier objectId)
        {
            var canLoad = typeId.Equals(ACTOR_TYPE_ID) && objectId is StringIdentifier;
            return canLoad;
        }

        public IGameObject Load(IIdentifier typeId, IIdentifier objectId)
        {
            // TODO: actually load one up based on the ID instead of generating a new one :)
            var actor = _actorFactory.Create();

            var identifier = actor.Behaviors.Get<IIdentifierBehavior>().Single();
            identifier.Id = objectId;

            var location = actor.Behaviors.Get<IWorldLocationBehavior>().Single();
            location.X = 40;
            location.Y = -25;

            return actor;
        }
    }
}
