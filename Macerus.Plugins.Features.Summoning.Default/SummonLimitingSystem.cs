using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Macerus.Plugins.Features.GameObjects.Actors;
using Macerus.Plugins.Features.Stats;

using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments;
using ProjectXyz.Plugins.Features.Mapping;
using ProjectXyz.Plugins.Features.TurnBased;

namespace Macerus.Plugins.Features.Summoning.Default
{
    public sealed class SummonLimitingSystem : IDiscoverableSystem
    {
        private readonly Lazy<ICombatTurnManager> _lazyCombatTurnManager;
        private readonly Lazy<IStatCalculationServiceAmenity> _lazyStatCalculationServiceAmenity;
        private readonly Lazy<IMapGameObjectManager> _lazyMapGameObjectManager;
        private readonly Lazy<IMacerusActorIdentifiers> _lazyMacerusActorIdentifiers;
        private readonly ISummonLimitStatPairRepositoryFacade _summonLimitStatPairRepository;
        private readonly Dictionary<IGameObject, ISummoningBehavior> _summoners;

        public SummonLimitingSystem(
            Lazy<ICombatTurnManager> lazyCombatTurnManager,
            Lazy<IStatCalculationServiceAmenity> lazyStatCalculationServiceAmenity,
            Lazy<IMapGameObjectManager> lazyMapGameObjectManager,
            Lazy<IMacerusActorIdentifiers> lazyMacerusActorIdentifiers,
            ISummonLimitStatPairRepositoryFacade summonLimitStatPairRepository)
        {
            _lazyCombatTurnManager = lazyCombatTurnManager;
            _lazyStatCalculationServiceAmenity = lazyStatCalculationServiceAmenity;
            _lazyMapGameObjectManager = lazyMapGameObjectManager;
            _lazyMacerusActorIdentifiers = lazyMacerusActorIdentifiers;
            _summonLimitStatPairRepository = summonLimitStatPairRepository;

            _summoners = new Dictionary<IGameObject, ISummoningBehavior>();

            // FIXME: no point in lazy because of this?
            _lazyMapGameObjectManager.Value.Synchronized += MapGameObjectManager_Synchronized;
        }

        public int? Priority => null;

        public async Task UpdateAsync(ISystemUpdateContext systemUpdateContext)
        {
            if (!_lazyCombatTurnManager.Value.InCombat)
            {
                return;
            }

            var actionInfo = systemUpdateContext
                .GetFirst<IComponent<IActionInfo>>()
                .Value;
            if (actionInfo.ElapsedActions < 1)
            {
                return;
            }

            foreach (var summonerEntry in _summoners)
            {
                await UnsummonExcessAsync(
                    summonerEntry.Key,
                    summonerEntry.Value)
                    .ConfigureAwait(false);
            }
        }

        private async Task UnsummonExcessAsync(
            IGameObject summoner,
            ISummoningBehavior summoningBehavior)
        {
            var summonLimitStatPairIds = GetBehaviorsFromEnchantmentSummonDefinitions<ISummonLimitStatBehavior>(summoner
                    .GetOnly<IReadOnlyHasEnchantmentsBehavior>()
                    .Enchantments)
                .Select(x => x.SummonLimitStatPairId)
                .Distinct()
                .ToArray();
            var summonLimitStatPairs = (await Task.WhenAll(summonLimitStatPairIds.Select(x => _summonLimitStatPairRepository.GetPairByIdAsync(x))))
                .ToDictionary(x => x.Id, x => x);
            var limitStatValues = await _lazyStatCalculationServiceAmenity
                .Value
                .GetStatValuesAsync(
                    summoner,
                    summonLimitStatPairs.Values.Select(x => x.MaximumStatDefinitionId))
                .ConfigureAwait(false);
            var summonerStatBehavior = summoner.GetOnly<IHasReadOnlyStatsBehavior>();
            foreach (var summonLimitStatPairEntry in summonLimitStatPairs)
            {
                var numberOfSummonsToUnsummon =
                    (int)(summonerStatBehavior.BaseStats[summonLimitStatPairEntry.Value.CurrentStatDefinitionId] -
                    limitStatValues[summonLimitStatPairEntry.Value.MaximumStatDefinitionId]);
                if (numberOfSummonsToUnsummon <= 0)
                {
                    continue;
                }

                var summonsByEnchantment = summoningBehavior.SummonsByEnchantment;
                var matchingEnchantmentsWithSummons = summonsByEnchantment
                    .Where(x => GetBehaviorsFromEnchantmentSummonDefinitions<ISummonLimitStatBehavior>(new[] { x.Key }).Any(statPairBehavior => Equals(
                        statPairBehavior.SummonLimitStatPairId,
                        summonLimitStatPairEntry.Key)))
                    .ToArray();
                if (matchingEnchantmentsWithSummons.Length < 1)
                {
                    continue;
                }

                var totalNumberOfUnsummoned = 0;
                foreach (var entry in matchingEnchantmentsWithSummons)
                {
                    var remaining = numberOfSummonsToUnsummon - totalNumberOfUnsummoned;
                    if (remaining <= 0)
                    {
                        break;
                    }

                    var unsummonedForEnchantment = await summoningBehavior
                        .UnsummonByEnchantmentAsync(
                            entry.Key,
                            remaining)
                        .ConfigureAwait(false);
                    totalNumberOfUnsummoned += unsummonedForEnchantment;
                }
            }
        }

        private IEnumerable<TBehavior> GetBehaviorsFromEnchantmentSummonDefinitions<TBehavior>(IEnumerable<IGameObject> enchantments)
            where TBehavior : IBehavior
        {
            var results = enchantments
                .Select(x => x.TryGetFirst<ISummonEnchantmentBehavior>(out var summonEnchantmentBehavior)
                    ? summonEnchantmentBehavior
                    : null)
                .Where(x => x != null)
                .Select(x => x.SummonDefinition.TryGetFirst<TBehavior>(out var xx)
                    ? xx
                    : default)
                .Where(x => x != null);
            return results;
        }

        private void MapGameObjectManager_Synchronized(
           object sender,
           GameObjectsSynchronizedEventArgs e)
        {
            foreach (var summonerEntry in e
                .Added
                .Where(x =>
                    x.TryGetFirst<ITypeIdentifierBehavior>(out var typeIdentifierBehavior) &&
                    Equals(typeIdentifierBehavior.TypeId, _lazyMacerusActorIdentifiers.Value.ActorTypeIdentifier))
                .Select(x => new
                {
                    Actor = x,
                    SummoningBehavior = x.TryGetFirst<ISummoningBehavior>(out var summoningBehavior)
                        ? summoningBehavior
                        : null,
                })
                .Where(x => x.SummoningBehavior != null))
            {
                _summoners[summonerEntry.Actor] = summonerEntry.SummoningBehavior;
            }

            foreach (var gameObject in e.Removed)
            {
                _summoners.Remove(gameObject);
            }
        }
    }
}
