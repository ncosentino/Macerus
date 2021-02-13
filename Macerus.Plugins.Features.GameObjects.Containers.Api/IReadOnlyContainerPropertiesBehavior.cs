using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Containers.Api
{
    public interface IReadOnlyContainerPropertiesBehavior : IBehavior
    {
        IReadOnlyDictionary<string, object> Properties { get; }
    }
}