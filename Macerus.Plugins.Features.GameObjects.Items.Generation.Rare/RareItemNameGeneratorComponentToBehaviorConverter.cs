using System;
using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Rare
{
    public sealed class RareItemNameGeneratorComponentToBehaviorConverter : IDiscoverableGeneratorComponentToBehaviorConverter
    {
        private readonly Lazy<IRareItemNameGenerator> _lazyRareItemNameGenerator;

        public RareItemNameGeneratorComponentToBehaviorConverter(Lazy<IRareItemNameGenerator> lazyRareItemNameGenerator)
        {
            _lazyRareItemNameGenerator = lazyRareItemNameGenerator;
        }

        public Type ComponentType => typeof(RareItemNameGeneratorComponent);

        public IEnumerable<IBehavior> Convert(
            IFilterContext filterContext,
            IEnumerable<IBehavior> baseBehaviors,
            IGeneratorComponent generatorComponent)
        {
            var nameComponent = _lazyRareItemNameGenerator.Value.GenerateName(
                baseBehaviors,
                baseBehaviors.GetOnly<IHasEnchantmentsBehavior>().Enchantments,
                filterContext);
            yield return nameComponent;
        }
    }
}