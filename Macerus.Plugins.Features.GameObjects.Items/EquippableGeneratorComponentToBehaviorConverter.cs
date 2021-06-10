using System;
using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Plugins.Features.GameObjects.Items
{
    public sealed class EquippableGeneratorComponentToBehaviorConverter : IDiscoverableGeneratorComponentToBehaviorConverter
    {
        public Type ComponentType { get; } = typeof(EquippableGeneratorComponent);

        public IEnumerable<IBehavior> Convert(
            IFilterContext filterContext,
            IEnumerable<IBehavior> baseBehaviors,
            IGeneratorComponent generatorComponent)
        {
            var equippableGeneratorComponent = (EquippableGeneratorComponent)generatorComponent;
            yield return new CanBeEquippedBehavior(equippableGeneratorComponent.AllowedEquipSlots);
        }
    }
}
