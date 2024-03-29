﻿using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.Combat.Api;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Filtering.Api;

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
            IFilterContext filterContext,
            IEnumerable<IBehavior> baseBehaviors,
            IGeneratorComponent generatorComponent)
        {
            var combatTeamGeneratorComponent = (ICombatTeamGeneratorComponent)generatorComponent;
            var hasStatsBehavior = baseBehaviors.GetFirst<IHasStatsBehavior>();

            hasStatsBehavior
                .MutateStatsAsync(async baseStats => baseStats[_combatTeamIdentifiers.CombatTeamStatDefinitionId] = combatTeamGeneratorComponent.Team)
                .Wait();

            return Enumerable.Empty<IBehavior>();
        }
    }
}
