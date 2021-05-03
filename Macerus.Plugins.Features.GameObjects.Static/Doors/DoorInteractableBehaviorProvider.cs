using System;
using System.Collections.Generic;
using System.Globalization;

using Macerus.Plugins.Features.GameObjects.Static.Api;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Static.Doors
{
    public sealed class DoorInteractableBehaviorProvider : IDiscoverableStaticGameObjectBehaviorsProvider
    {
        public IEnumerable<IBehavior> GetBehaviors(IReadOnlyCollection<IBehavior> baseBehaviors)
        {
            var properties = baseBehaviors.GetOnly<IReadOnlyStaticGameObjectPropertiesBehavior>();
            if (properties.Properties.ContainsKey("DoorBehavior"))
            {
                var doorBehaviorProperties = (IReadOnlyDictionary<string, object>)properties.Properties["DoorBehavior"];

                var doorBehavior = new DoorInteractableBehavior(
                    doorBehaviorProperties.TryGetValue("AutomaticInteraction", out var automaticInteraction) &&
                        Convert.ToBoolean(automaticInteraction, CultureInfo.InvariantCulture),
                    doorBehaviorProperties.TryGetValue("TransitionToMapId", out var transitionToMapId)
                        ? new StringIdentifier(Convert.ToString(transitionToMapId, CultureInfo.InvariantCulture))
                        : null,
                    doorBehaviorProperties.TryGetValue("TransitionToX", out var transitionToX)
                        ? Convert.ToDouble(transitionToX, CultureInfo.InvariantCulture)
                        : (double?)null,
                    doorBehaviorProperties.TryGetValue("TransitionToY", out var transitionToY)
                        ? Convert.ToDouble(transitionToY, CultureInfo.InvariantCulture)
                        : (double?)null);
                yield return doorBehavior;
            }

            yield break;
        }
    }
}
