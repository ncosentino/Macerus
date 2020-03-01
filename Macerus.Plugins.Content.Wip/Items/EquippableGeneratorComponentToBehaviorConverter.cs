using System;
using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.CommonBehaviors;

namespace Macerus.Plugins.Content.Wip.Items
{
    public sealed class EquippableGeneratorComponentToBehaviorConverter : IDiscoverableGeneratorComponentToBehaviorConverter
    {
        public Type ComponentType { get; } = typeof(EquippableGeneratorComponent);

        public IEnumerable<IBehavior> Convert(IGeneratorComponent generatorComponent)
        {
            var equippableGeneratorComponent = (EquippableGeneratorComponent)generatorComponent;
            yield return new CanBeEquippedBehavior(equippableGeneratorComponent.AllowedEquipSlots);
        }
    }
}
