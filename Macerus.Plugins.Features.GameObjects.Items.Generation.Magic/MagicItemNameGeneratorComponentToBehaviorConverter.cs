using System;
using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Magic
{
    public sealed class MagicItemNameGeneratorComponentToBehaviorConverter : IDiscoverableGeneratorComponentToBehaviorConverter
    {
        private readonly Lazy<IMagicItemNameGenerator> _lazyMagicItemNameGenerator;

        public MagicItemNameGeneratorComponentToBehaviorConverter(Lazy<IMagicItemNameGenerator> lazyMagicItemNameGenerator)
        {
            _lazyMagicItemNameGenerator = lazyMagicItemNameGenerator;
        }

        public Type ComponentType => typeof(MagicItemNameGeneratorComponent);

        public IEnumerable<IBehavior> Convert(
            IFilterContext filterContext,
            IEnumerable<IBehavior> baseBehaviors,
            IGeneratorComponent generatorComponent)
        {
            var nameComponent = _lazyMagicItemNameGenerator.Value.GenerateName(
                baseBehaviors,
                baseBehaviors.GetOnly<IHasEnchantmentsBehavior>().Enchantments);
            yield return nameComponent;
        }
    }
}