using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Macerus.Plugins.Features.GameObjects.Actors.Triggers;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Shared.Behaviors.Triggering
{
    public sealed class TriggerEnchantmentOnDeathBehavior :
        BaseBehavior,
        IDeathTriggerMechanic
    {
        private readonly IDeathTriggerMechanicSource _deathTriggerMechanicSource;
        private readonly IAttributeFilterer _attributeFilterer;
        private readonly IReadOnlyCollection<IGameObject> _enchantments;
        private readonly IReadOnlyCollection<IFilterAttributeValue> _filter;

        public TriggerEnchantmentOnDeathBehavior(
            IDeathTriggerMechanicSource deathTriggerMechanicSource,
            IAttributeFilterer attributeFilterer,
            IEnumerable<IFilterAttributeValue> filter,
            IEnumerable<IGameObject> enchantments)
        {
            _deathTriggerMechanicSource = deathTriggerMechanicSource;
            _attributeFilterer = attributeFilterer;
            _filter = filter.ToArray();
            _enchantments = enchantments.ToArray();
        }

        public async Task ActorDeathTriggeredAsync(IGameObject actor)
        {
            if (!_attributeFilterer.IsMatch(actor, _filter))
            {
                return;
            }

            await actor
                .GetOnly<IHasEnchantmentsBehavior>()
                .AddEnchantmentsAsync(_enchantments)
                .ConfigureAwait(false);
        }

        protected override void OnRegisteredToOwner(IGameObject owner)
        {
            _deathTriggerMechanicSource.UnregisterTrigger(this);
            base.OnRegisteredToOwner(owner);

            if (owner != null)
            {
                _deathTriggerMechanicSource.RegisterTrigger(this);
            }
        }
    }
}