using System.Collections.Generic;

using Macerus.Plugins.Features.GameObjects.Actors.Api;
using Macerus.Shared.Behaviors;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;

namespace Macerus.Plugins.Features.GameObjects.Actors
{
    public sealed class ActorBehaviorsProvider : IDiscoverableActorBehaviorsProvider
    {
        private readonly IDynamicAnimationBehaviorFactory _dynamicAnimationBehaviorFactory;

        public ActorBehaviorsProvider(IDynamicAnimationBehaviorFactory dynamicAnimationBehaviorFactory)
        {
            _dynamicAnimationBehaviorFactory = dynamicAnimationBehaviorFactory;
        }

        public IEnumerable<IBehavior> GetBehaviors(IReadOnlyCollection<IBehavior> baseBehaviors)
        {
            yield return new WorldLocationBehavior();
            yield return _dynamicAnimationBehaviorFactory.Create("$actor$");
            yield return new MovementBehavior();
        }
    }
}
