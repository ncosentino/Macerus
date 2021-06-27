using System.Collections.Generic;

using Macerus.Api.Behaviors;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;

namespace Macerus.Plugins.Features.GameObjects.Actors
{
    public sealed class InitialStateActorBehaviorsInterceptor : IDiscoverableActorBehaviorsInterceptor
    {
        private readonly IActorIdentifiers _actorIdentifiers;

        public InitialStateActorBehaviorsInterceptor(IActorIdentifiers actorIdentifiers)
        {
            _actorIdentifiers = actorIdentifiers;
        }

        public int Priority => -10000;

        IEnumerable<IBehavior> IActorBehaviorsInterceptor.Intercept(IReadOnlyCollection<IBehavior> behaviors)
        {
            var dynamicAnimationBehavior = behaviors.GetOnly<IDynamicAnimationBehavior>();
            var movementBehavior = behaviors.GetOnly<IMovementBehavior>();
            movementBehavior.Direction = 3;
            dynamicAnimationBehavior.BaseAnimationId = _actorIdentifiers.AnimationStand;
            return behaviors;
        }
    }
}
