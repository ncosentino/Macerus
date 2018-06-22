using System;
using System.Collections.Generic;
using Macerus.Shared.Behaviors;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Actors
{
    public sealed class AdditionalActorBehaviorsProvider : IAdditionalActorBehaviorsProvider
    {
        public IEnumerable<IBehavior> GetBehaviors(IGameObject gameObject)
        {
            yield return new WorldLocationBehavior();
            yield return new MovementBehavior();
            yield return new ItemContainerBehavior(new StringIdentifier("Inventory"));
            yield return new ItemContainerBehavior(new StringIdentifier("Belt"));
        }
    }
}
