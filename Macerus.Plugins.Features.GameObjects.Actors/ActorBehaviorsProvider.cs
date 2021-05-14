using System.Collections.Generic;

using Macerus.Plugins.Features.GameObjects.Actors.Api;
using Macerus.Shared.Behaviors;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Shared.Framework;

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
            yield return new WorldLocationBehavior()
            {
                Width = 1,
                Height = 1,
            };
            yield return _dynamicAnimationBehaviorFactory.Create(
                "$actor$",
                new StringIdentifier(string.Empty),
                true,
                0);
            yield return new MovementBehavior();
        }
    }
}
