using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using NexusLabs.Collections.Generic;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.Summoning.Default
{
    public sealed class SummoningBehavior :
        BaseBehavior,
        ISummoningBehavior
    {
        private readonly Dictionary<IGameObject, ISummoningContext> _summoningContextsByEnchantment;
        private readonly ISummonHandlerFacade _summonHandlerFacade;

        private IObservableHasEnchantmentsBehavior _hasEnchantmentsBehavior;

        public event EventHandler<SummonEventArgs> SummonedAsync;

        public event EventHandler<SummonEventArgs> UnsummonedAsync;

        public SummoningBehavior(ISummonHandlerFacade summonHandlerFacade)
        {
            _summoningContextsByEnchantment = new Dictionary<IGameObject, ISummoningContext>();
            _summonHandlerFacade = summonHandlerFacade;
        }

        public IReadOnlyDictionary<IGameObject, IFrozenCollection<IGameObject>> SummonsByEnchantment =>
            _summoningContextsByEnchantment.ToDictionary(
                x => x.Key,
                x => x.Value.Summons.AsFrozenCollection());

        public async Task<int> UnsummonByEnchantmentAsync(IGameObject enchantment, int count)
        {
            if (!_summoningContextsByEnchantment.TryGetValue(
                enchantment,
                out var summoningContext))
            {
                return 0;
            }

            var summonsToUnsummon = summoningContext
                .Summons
                .Take(count)
                .AsFrozenCollection();

            var newContext = new SummoningContext(
                summoningContext.Summoner,
                summoningContext.SummoningEnchantment,
                summoningContext.Summons.Except(summonsToUnsummon));
            _summoningContextsByEnchantment[enchantment] = newContext;

            await UnsummonedAsync
                .InvokeOrderedAsync(
                    this,
                    new SummonEventArgs(summonsToUnsummon))
                .ConfigureAwait(false);
            return summonsToUnsummon.Count;
        }

        protected override void OnRegisteredToOwner(IGameObject owner)
        {
            base.OnRegisteredToOwner(owner);
            SetupHasEnchantmentsBehavior(owner);
        }

        private void SetupHasEnchantmentsBehavior(IGameObject owner)
        {
            if (_hasEnchantmentsBehavior != null)
            {
                _hasEnchantmentsBehavior.EnchantmentsChanged -= HasEnchantmentsBehavior_EnchantmentsChanged;
            }

            _hasEnchantmentsBehavior = owner?.TryGetFirst(out _hasEnchantmentsBehavior) == true
                ? _hasEnchantmentsBehavior
                : null;

            if (_hasEnchantmentsBehavior != null)
            {
                _hasEnchantmentsBehavior.EnchantmentsChanged += HasEnchantmentsBehavior_EnchantmentsChanged;
            }
        }

        private async void HasEnchantmentsBehavior_EnchantmentsChanged(
            object sender,
            EnchantmentsChangedEventArgs e)
        {
            var removedSummonEnchantmentBehaviors = e
                .RemovedEnchantments
                .SelectMany(x => x
                    .Behaviors
                    .TakeTypes<ISummonEnchantmentBehavior>());
            // TODO: remove summons......

            var addedSummonEnchantmentBehaviors = e
                .AddedEnchantments
                .SelectMany(x => x
                    .Behaviors
                    .TakeTypes<ISummonEnchantmentBehavior>());
            await HandleAddedSummonEnchantmentsAsync(addedSummonEnchantmentBehaviors).ConfigureAwait(false);
        }

        private async Task HandleAddedSummonEnchantmentsAsync(IEnumerable<ISummonEnchantmentBehavior> addedSummonEnchantmentBehaviors)
        {
            var newSummons = new List<IGameObject>();
            foreach (var summonEnchantmentBehavior in addedSummonEnchantmentBehaviors)
            {
                ISummoningContext summoningContext = new SummoningContext(
                    _hasEnchantmentsBehavior.Owner,
                    summonEnchantmentBehavior.Owner,
                    Enumerable.Empty<IGameObject>());
                summoningContext = await _summonHandlerFacade
                    .HandleSummoningAsync(summoningContext)
                    .ConfigureAwait(false);
                _summoningContextsByEnchantment[summonEnchantmentBehavior.Owner] = summoningContext;

                newSummons.AddRange(summoningContext.Summons);
            }

            if (newSummons.Any())
            {
                await SummonedAsync
                    .InvokeOrderedAsync(
                        this,
                        new SummonEventArgs(newSummons.AsFrozenCollection()))
                    .ConfigureAwait(false);
            }
        }

        private async Task HandleRemovedSummonEnchantmentsAsync(IEnumerable<ISummonEnchantmentBehavior> removedSummonEnchantmentBehaviors)
        {
            var removedSummons = new List<IGameObject>();
            foreach (var summonEnchantmentBehavior in removedSummonEnchantmentBehaviors)
            {
                if (!_summoningContextsByEnchantment.TryGetValue(
                    summonEnchantmentBehavior.Owner,
                    out var summoningContext))
                {
                    continue;
                }

                removedSummons.AddRange(summoningContext.Summons);
            }

            if (removedSummons.Any())
            {
                await UnsummonedAsync
                    .InvokeOrderedAsync(
                        this,
                        new SummonEventArgs(removedSummons.AsFrozenCollection()))
                    .ConfigureAwait(false);
            }
        }
    }
}
