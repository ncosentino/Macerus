using System.Collections.Generic;

using Macerus.Plugins.Features.GameObjects.Static.Api;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.Encounters.Triggers.GamObjects.Static
{
    public sealed class EncounterSpawnLocationBehaviorsProvider : IDiscoverableStaticGameObjectBehaviorsProvider
    {
        private static readonly IIdentifier ENCOUNTER_SPAWN_ID = new StringIdentifier("encounterspawn");

        public IEnumerable<IBehavior> GetBehaviors(IReadOnlyCollection<IBehavior> baseBehaviors)
        {
            if (!baseBehaviors
                .GetOnly<IReadOnlyTemplateIdentifierBehavior>()
                .TemplateId
                .Equals(ENCOUNTER_SPAWN_ID))
            {
                yield break;
            }

            var baseProperties = baseBehaviors.GetFirst<IReadOnlyStaticGameObjectPropertiesBehavior>();
            yield return new EncounterSpawnLocationBehavior(); 
        }
    }
}
