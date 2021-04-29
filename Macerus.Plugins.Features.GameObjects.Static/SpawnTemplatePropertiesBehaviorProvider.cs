using System.Collections.Generic;

using Macerus.Plugins.Features.GameObjects.Static.Api;
using Macerus.Shared.Behaviors;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.GameObjects.Static
{
    public sealed class SpawnTemplatePropertiesBehaviorProvider : IDiscoverableStaticGameObjectBehaviorsProvider
    {
        public IEnumerable<IBehavior> GetBehaviors(IReadOnlyCollection<IBehavior> baseBehaviors)
        {
            var properties = baseBehaviors.GetOnly<IReadOnlyStaticGameObjectPropertiesBehavior>();
            if (properties.Properties.TryGetValue(
                "SpawnTemplateProperties",
                out var rawSpawnTemplateProperties))
            {
                var castedRawSpawnTemplateProperties = (IReadOnlyDictionary<string, object>)rawSpawnTemplateProperties;
                var spawnTemplatePropertiesBehavior = new SpawnTemplatePropertiesBehavior(castedRawSpawnTemplateProperties);
                yield return spawnTemplatePropertiesBehavior;
            }

            yield break;
        }
    }
}
