using System.Collections.Generic;

using Macerus.Plugins.Features.GameObjects.Containers.Api;

using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Containers
{
    public sealed class ContainerPropertiesBehavior :
        BaseBehavior,
        IReadOnlyContainerPropertiesBehavior
    {
        public ContainerPropertiesBehavior(IReadOnlyDictionary<string, object> properties)
        {
            Properties = properties;
        }

        public IReadOnlyDictionary<string, object> Properties { get; }
    }
}
