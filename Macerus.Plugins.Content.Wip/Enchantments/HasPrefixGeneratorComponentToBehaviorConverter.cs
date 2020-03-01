using ProjectXyz.Api.GameObjects.Generation;
using System;
using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;
using Macerus.Plugins.Features.GameObjects.Enchantments.Generation.Magic;

namespace Macerus.Plugins.Content.Wip.Enchantments
{
    public sealed class HasPrefixGeneratorComponentToBehaviorConverter : IDiscoverableGeneratorComponentToBehaviorConverter
    {
        public Type ComponentType { get; } = typeof(HasPrefixGeneratorComponent);

        public IEnumerable<IBehavior> Convert(IGeneratorComponent generatorComponent)
        {
            var hasPrefixGeneratorComponent = (HasPrefixGeneratorComponent)generatorComponent;
            yield return new HasPrefixBehavior(hasPrefixGeneratorComponent.PrefixId);
        }
    }
}
