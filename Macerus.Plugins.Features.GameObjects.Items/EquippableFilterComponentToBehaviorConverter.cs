using System;
using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Plugins.Features.CommonBehaviors;

namespace Macerus.Plugins.Features.GameObjects.Items
{
    public sealed class EquippableFilterComponentToBehaviorConverter : IDiscoverableFilterComponentToBehaviorConverter
    {
        public Type ComponentType { get; } = typeof(EquippableFilterComponent);

        public IEnumerable<IBehavior> Convert(IFilterComponent FilterComponent)
        {
            var equippableFilterComponent = (EquippableFilterComponent)FilterComponent;
            yield return new CanBeEquippedBehavior(equippableFilterComponent.AllowedEquipSlots);
        }
    }
}
