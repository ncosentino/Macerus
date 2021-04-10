﻿using System.Collections.Generic;
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

        public bool CheckWinConditions(
            out IReadOnlyCollection<IGameObject> winningTeam,
            out IReadOnlyDictionary<int, IReadOnlyCollection<IGameObject>> losingTeams)
        {
            var teamMapping = _combatGameObjectProvider
                .GetGameObjects()
                .GroupBy(actor =>
                {
                    var baseTeam = (int)actor
                        .GetOnly<IHasStatsBehavior>()
                        .BaseStats[_combatTeamIdentifiers.CombatTeamStatDefinitionId];
                    return baseTeam;
                })
                .ToDictionary(
                    team => team.Key,
                    team => team.ToReadOnlyCollection());
            var aliveTeamMapping = teamMapping
                .ToDictionary(
                    team => team.Key,
                    team => team.Value
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
            var remainingTeams = aliveTeamMapping
                .Where(team => team.Value.Count > 0)
                .Select(x => x.Key)
                .ToArray();
            var onlyOneRemainingTeam = remainingTeams.Length == 1;
            if (onlyOneRemainingTeam)
            {
                winningTeam = teamMapping[remainingTeams.Single()];
                losingTeams = teamMapping
                    .Where(x => x.Key != remainingTeams.Single())
                    .ToDictionary(x => x.Key, ProjectXyz => ProjectXyz.Value);
            }
            else
            {
                winningTeam = new IGameObject[0];
                losingTeams = new Dictionary<int, IReadOnlyCollection<IGameObject>>();
            }

            return onlyOneRemainingTeam;
        }
    }
}