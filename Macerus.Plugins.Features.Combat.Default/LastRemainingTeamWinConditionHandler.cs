using System.Linq;

using Macerus.Plugins.Features.Combat.Api;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.Combat.Default
{
    public sealed class LastRemainingTeamWinConditionHandler : IDiscoverableWinConditionHandler
    {
        private readonly ICombatGameObjectProvider _combatGameObjectProvider;
        private readonly ICombatTeamIdentifiers _combatTeamIdentifiers;

        public LastRemainingTeamWinConditionHandler(
            ICombatGameObjectProvider combatGameObjectProvider,
            ICombatTeamIdentifiers combatTeamIdentifiers)
        {
            _combatGameObjectProvider = combatGameObjectProvider;
            _combatTeamIdentifiers = combatTeamIdentifiers;
        }

        public bool TryGetWinningTeam(out double winningTeamId)
        {
            var mapping = _combatGameObjectProvider
                .GetGameObjects()
                .GroupBy(actor =>
                {
                    var baseTeam = actor
                        .GetOnly<IHasStatsBehavior>()
                        .BaseStats[_combatTeamIdentifiers.CombatTeamStatDefinitionId];
                    return baseTeam;
                })
                .ToDictionary(
                    team => team.Key,
                    team => team
                        .Where(actor =>
                        {
                            var currentLife = actor
                                .GetOnly<IHasStatsBehavior>()
                                // FIXME: this is current life (also do we
                                // need to consider a stat calc or can we assume
                                // base stat)
                                .BaseStats[new IntIdentifier(1)];
                            return currentLife > 0;
                        })
                        .ToReadOnlyCollection());
            var remainingTeams = mapping
                .Where(team => team.Value.Count > 0)
                .Select(x => x.Key)
                .ToArray();
            var onlyOneRemainingTeam = remainingTeams.Length == 1;
            winningTeamId = onlyOneRemainingTeam
                ? remainingTeams.Single()
                : -1;
            return onlyOneRemainingTeam;
        }
    }
}
