using System;
using System.Collections.Generic;

using Macerus.Plugins.Features.GameObjects.Items.Generation.Unique;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Content.Wip.Items
{
    public sealed class UniqueBaseItemGeneratorComponentToBehaviorConverter : IDiscoverableGeneratorComponentToBehaviorConverter
    {
        public Type ComponentType { get; } = typeof(UniqueBaseItemGeneratorComponent);

        public IEnumerable<IBehavior> Convert(IGeneratorComponent generatorComponent)
        {
            var identifierGeneratorComponent = (UniqueBaseItemGeneratorComponent)generatorComponent;
            yield return new UniqueBaseItemBehavior(identifierGeneratorComponent.Identifier);
        }
    }
}
