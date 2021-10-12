using System;
using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Magic
{
    public sealed class MagicItemAffixGeneratorComponentToBehaviorConverter : IDiscoverableGeneratorComponentToBehaviorConverter
    {
        public Type ComponentType => typeof(MagicItemAffixGeneratorComponent);

        public IEnumerable<IBehavior> Convert(
            IFilterContext filterContext,
            IEnumerable<IBehavior> baseBehaviors,
            IGeneratorComponent generatorComponent)
        {
            var magicItemAffixGeneratorComponent = (MagicItemAffixGeneratorComponent)generatorComponent;
            yield return new HasMagicAffixBehavior(
                magicItemAffixGeneratorComponent.PrefixStringResourceId,
                magicItemAffixGeneratorComponent.SuffixStringResourceId);
        }
    }
}
