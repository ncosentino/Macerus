using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Macerus.Plugins.Features.GameObjects.Static.Api;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.Encounters.Triggers.GamObjects.Static
{
    public sealed class EncounterSpawnLocationBehaviorsProvider : IDiscoverableStaticGameObjectBehaviorsProvider
    {
        private static readonly IIdentifier ENCOUNTER_SPAWN_ID = new StringIdentifier("EncounterSpawn");

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
            baseProperties.Properties.TryGetValue("AllowedTeams", out var rawTeamsObject);
            var rawTeams = (string)rawTeamsObject;

            var allowedTeamIds = string.IsNullOrWhiteSpace(rawTeams)
                ? new int[0]
                : rawTeams
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt32(x, CultureInfo.InvariantCulture));

            yield return new EncounterSpawnLocationBehavior(allowedTeamIds); 
        }
    }
}
