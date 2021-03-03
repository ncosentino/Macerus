using System.Collections.Generic;

using Macerus.Shared.Behaviors;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;

namespace Macerus.Plugins.Features.GameObjects.Actors
{
    public sealed class ActorBehaviorsProvider : IDiscoverableActorBehaviorsProvider
    {
        public IEnumerable<IBehavior> GetBehaviors(IReadOnlyCollection<IBehavior> baseBehaviors)
        {
            yield return new WorldLocationBehavior();
            yield return new AnimationBehavior();
            yield return new MovementBehavior();
        }
    }
}
