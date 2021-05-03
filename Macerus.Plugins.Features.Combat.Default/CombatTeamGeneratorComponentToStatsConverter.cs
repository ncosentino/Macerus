using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.Combat.Api;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Macerus.Plugins.Features.Combat.Default
{
    public sealed class CombatTeamGeneratorComponentToStatsConverter : IDiscoverableGeneratorComponentToBehaviorConverter
    {
        private readonly ICombatTeamIdentifiers _combatTeamIdentifiers;

        public CombatTeamGeneratorComponentToStatsConverter(ICombatTeamIdentifiers combatTeamIdentifiers)
        {
            _combatTeamIdentifiers = combatTeamIdentifiers;
        }

        public Type ComponentType => typeof(CombatTeamGeneratorComponent);

        public IEnumerable<IBehavior> Convert(
            IEnumerable<IBehavior> baseBehaviors,
            IGeneratorComponent generatorComponent)
        {
            var combatTeamGeneratorComponent = (ICombatTeamGeneratorComponent)generatorComponent;
            var hasMutableStatsBehavior = baseBehaviors.GetFirst<IHasMutableStatsBehavior>();

            hasMutableStatsBehavior.MutateStats(baseStats =>
                baseStats[_combatTeamIdentifiers.CombatTeamStatDefinitionId] = combatTeamGeneratorComponent.Team);

            return Enumerable.Empty<IBehavior>();
        }
    }
}
