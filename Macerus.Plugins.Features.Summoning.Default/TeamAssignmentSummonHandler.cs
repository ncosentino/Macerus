using System;
using System.Threading.Tasks;

using Macerus.Plugins.Features.Combat.Api;

using NexusLabs.Framework;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Macerus.Plugins.Features.Summoning.Default
{
    public sealed class TeamAssignmentSummonHandler : IDiscoverableSummonHandler
    {
        private readonly Lazy<ICombatTeamIdentifiers> _lazyCombatTeamIdentifiers;
        private readonly IRandom _random;

        public TeamAssignmentSummonHandler(
            Lazy<ICombatTeamIdentifiers> lazyCombatTeamIdentifiers,
            IRandom random)
        {
            _lazyCombatTeamIdentifiers = lazyCombatTeamIdentifiers;
            _random = random;
        }

        public int? Priority => 30000;

        public async Task<ISummoningContext> HandleSummoningAsync(ISummoningContext summoningContext)
        {
            var summonDefinition = summoningContext
                .SummonEnchantmentBehavior
                .SummonDefinition;
            foreach (var summon in summoningContext.Summons)
            {
                var summonStatsBehavior = summon.GetOnly<IHasStatsBehavior>();

                // no behavior? inherit from summoner
                if (!summonDefinition.TryGetFirst<ISummonTeamAssignmentBehavior>(out var summonTeamAssingmentBehavior) ||
                    summonTeamAssingmentBehavior is SummonSameTeamAsSummonerBehavior)
                {
                    // FIXME: if we have a summoner currently with their
                    // combat team temporarily altered (i.e. through some
                    // sort of enchantment), should we respect that here? how
                    // does that work when the summoner changes teams again
                    // later?
                    await summonStatsBehavior
                        .MutateStatsAsync(async summonStats => 
                            summonStats[_lazyCombatTeamIdentifiers.Value.CombatTeamStatDefinitionId] = 
                            summoningContext.SummonerStatsBehavior.BaseStats[_lazyCombatTeamIdentifiers.Value.CombatTeamStatDefinitionId])
                        .ConfigureAwait(false);
                    continue;
                }

                // FIXME: switch to some type of facade for extensibility?
                if (summonTeamAssingmentBehavior is SummonNeutralTeamBehavior)
                {
                    await summonStatsBehavior
                        .MutateStatsAsync(async summonStats =>
                            summonStats[_lazyCombatTeamIdentifiers.Value.CombatTeamStatDefinitionId] = 
                            _random.NextDouble(100000, 1000000))
                        .ConfigureAwait(false);
                }
                else if (summonTeamAssingmentBehavior is SummonRandomTeamBehavior)
                {
                    throw new NotSupportedException(
                       $"Summon team assignment behavior " +
                       $"'{summonTeamAssingmentBehavior}' is not yet supported. " +
                       $"You could be the hereo that implements this.");
                }
                else
                {
                    throw new NotSupportedException(
                        $"Summon team assignment behavior " +
                        $"'{summonTeamAssingmentBehavior}' is not supported. " +
                        $"Consider switching to a facade implementation to " +
                        $"handle more extensibility if needed.");
                }
            };
            
            return summoningContext;
        }
    }
}
