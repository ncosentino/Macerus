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
            yield return new AnimationBehavior();
            yield return new ItemContainerBehavior(new StringIdentifier("Inventory"));
            yield return new ItemContainerBehavior(new StringIdentifier("Belt"));
            yield return new CanEquipBehavior(new[]
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
            });
        }
    }
}
