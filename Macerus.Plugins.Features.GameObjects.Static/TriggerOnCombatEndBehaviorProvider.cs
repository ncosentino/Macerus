using System.Collections.Generic;

using Macerus.Plugins.Features.GameObjects.Static.Api;
using Macerus.Shared.Behaviors;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.GameObjects.Static
{
    public sealed class TriggerOnCombatEndBehaviorProvider : IDiscoverableStaticGameObjectBehaviorsProvider
    {
        public IEnumerable<IBehavior> GetBehaviors(IReadOnlyCollection<IBehavior> baseBehaviors)
        {
            var properties = baseBehaviors.GetOnly<IReadOnlyStaticGameObjectPropertiesBehavior>();
            if (properties.Properties.ContainsKey("TriggerOnCombatEnd"))
            {
                var triggerOnCombatEndBehavior = new TriggerOnCombatEndBehavior();
                yield return triggerOnCombatEndBehavior;
            }

            yield break;
        }
    }
}
