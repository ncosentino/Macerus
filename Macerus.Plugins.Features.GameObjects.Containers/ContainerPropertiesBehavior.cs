using System;
using System.Collections.Generic;
using System.Globalization;

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
            RawProperties = properties;
        }

        public IReadOnlyDictionary<string, object> RawProperties { get; }

        public double X => Convert.ToDouble(RawProperties["X"], CultureInfo.InvariantCulture);
        
        public double Y => Convert.ToDouble(RawProperties["Y"], CultureInfo.InvariantCulture);
        
        public double Width => Convert.ToDouble(RawProperties["Width"], CultureInfo.InvariantCulture);
        
        public double Height => Convert.ToDouble(RawProperties["Height"], CultureInfo.InvariantCulture);

        public bool DestroyOnUse => RawProperties
            .TryGetValue(
                "DestroyOnUse",
                out var rawDestroyOnUse) == true &&
            Convert.ToBoolean(
                rawDestroyOnUse,
                CultureInfo.InvariantCulture);

        public bool AutomaticInteraction => RawProperties
            .TryGetValue(
                "AutomaticInteraction",
                out var rawDestroyOnUse) == true &&
            Convert.ToBoolean(
                rawDestroyOnUse,
                CultureInfo.InvariantCulture);

        public bool Collisions => RawProperties
            .TryGetValue(
                "Collisions",
                out var rawDestroyOnUse) == true &&
            Convert.ToBoolean(
                rawDestroyOnUse,
                CultureInfo.InvariantCulture);
    }
}
