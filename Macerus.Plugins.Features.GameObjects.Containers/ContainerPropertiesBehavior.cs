using System;
using System.Collections.Generic;
using System.Globalization;

using Macerus.Plugins.Features.GameObjects.Containers.Api;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;
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
                out var rawValue) == true &&
            Convert.ToBoolean(
                rawValue,
                CultureInfo.InvariantCulture);

        public bool AutomaticInteraction => RawProperties
            .TryGetValue(
                "AutomaticInteraction",
                out var rawValue) == true &&
            Convert.ToBoolean(
                rawValue,
                CultureInfo.InvariantCulture);

        public bool Collisions => RawProperties
            .TryGetValue(
                "Collisions",
                out var rawValue) == true &&
            Convert.ToBoolean(
                rawValue,
                CultureInfo.InvariantCulture);

        public bool TransferItemsOnActivate => RawProperties
            .TryGetValue(
                "TransferItemsOnActivate",
                out var rawValue) == true &&
            Convert.ToBoolean(
                rawValue,
                CultureInfo.InvariantCulture);

        public IIdentifier DropTableId
        {
            get
            {
                if (!RawProperties.TryGetValue(
                    "DropTableId",
                    out var rawValue))
                {
                    return null;
                }
                
                var id = new StringIdentifier(Convert.ToString(
                    rawValue,
                    CultureInfo.InvariantCulture));
                return id;
            }
        }
    }
}
