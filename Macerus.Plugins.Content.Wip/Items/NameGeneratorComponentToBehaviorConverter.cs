using System;
using System.Collections.Generic;

using Macerus.Plugins.Features.GameObjects.Items.Behaviors;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Content.Wip.Items
{
    public sealed class NameGeneratorComponentToBehaviorConverter : IDiscoverableGeneratorComponentToBehaviorConverter
    {
        public Type ComponentType { get; } = typeof(NameGeneratorComponent);

        public IEnumerable<IBehavior> Convert(IGeneratorComponent generatorComponent)
        {
            var nameGeneratorComponent = (NameGeneratorComponent)generatorComponent;
            yield return new HasInventoryDisplayName(nameGeneratorComponent.DisplayName);
        }
    }
}
