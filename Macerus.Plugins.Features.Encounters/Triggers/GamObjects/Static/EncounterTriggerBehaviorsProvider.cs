using System.Collections.Generic;

using Macerus.Plugins.Features.GameObjects.Static.Api;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.Encounters.Triggers.GamObjects.Static
{
    public sealed class EncounterTriggerBehaviorsProvider : IDiscoverableStaticGameObjectBehaviorsProvider
    {
        private static readonly IIdentifier ENCOUNTER_TRIGGER_TEMPLATE_ID = new StringIdentifier("EncounterTrigger");

        public IEnumerable<IBehavior> GetBehaviors(IReadOnlyCollection<IBehavior> baseBehaviors)
        {
            if (!baseBehaviors
                .GetOnly<IReadOnlyTemplateIdentifierBehavior>()
                .TemplateId
                .Equals(ENCOUNTER_TRIGGER_TEMPLATE_ID))
            {
                yield break;
            }

            var baseProperties = baseBehaviors.GetFirst<IReadOnlyStaticGameObjectPropertiesBehavior>();
            var properties = new EncounterTriggerPropertiesBehavior(baseProperties);
            yield return properties;
        }
    }
}
