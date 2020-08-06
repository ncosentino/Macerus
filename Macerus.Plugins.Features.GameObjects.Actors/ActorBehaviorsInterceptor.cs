using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Actors
{
    public sealed class ActorBehaviorsInterceptor : IActorBehaviorsInterceptor
    {
        public void Intercept(ICollection<IBehavior> behaviors)
        {
            var mutableStats = behaviors.GetOnly<IHasMutableStatsBehavior>();
            mutableStats.MutateStats(stats =>
            {
                stats[new StringIdentifier("Light Radius Radius")] = 10;
                stats[new StringIdentifier("Light Radius Intensity")] = 0.5;
                stats[new StringIdentifier("Light Radius Red")] = 1;
                stats[new StringIdentifier("Light Radius Green")] = 0;
                stats[new StringIdentifier("Light Radius Blue")] = 1;
            });
        }
    }
}
