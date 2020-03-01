using ProjectXyz.Api.GameObjects.Generation;
using System;
using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;
using Macerus.Plugins.Features.GameObjects.Enchantments.Generation.Magic;

namespace Macerus.Plugins.Content.Wip.Enchantments
{
    public sealed class HasSuffixGeneratorComponentToBehaviorConverter : IDiscoverableGeneratorComponentToBehaviorConverter
    {
        public Type ComponentType { get; } = typeof(HasSuffixGeneratorComponent);

        public IEnumerable<IBehavior> Convert(IGeneratorComponent generatorComponent)
        {
            var hasSuffixGeneratorComponent = (HasSuffixGeneratorComponent)generatorComponent;
            yield return new HasSuffixBehavior(hasSuffixGeneratorComponent.SuffixId);
        }
    }
}
