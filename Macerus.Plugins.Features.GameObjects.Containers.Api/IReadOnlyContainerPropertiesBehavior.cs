using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Containers.Api
{
    public interface IReadOnlyContainerPropertiesBehavior : IBehavior
    {
        IReadOnlyDictionary<string, object> RawProperties { get; }

        double X { get; }

        double Y { get; }

        double Width { get; }

        double Height { get; }

        bool DestroyOnUse { get; }

        bool AutomaticInteraction { get; }

        bool Collisions { get; }
    }
}