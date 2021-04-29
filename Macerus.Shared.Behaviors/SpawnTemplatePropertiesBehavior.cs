using System;
using System.Collections.Generic;
using System.Globalization;

using Macerus.Api.Behaviors;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Shared.Behaviors
{
    public sealed class SpawnTemplatePropertiesBehavior :
        BaseBehavior,
        IReadOnlySpawnTemplatePropertiesBehavior
    {
        public SpawnTemplatePropertiesBehavior(IReadOnlyDictionary<string, object> properties)
        {
            Properties = properties;
        }

        public IReadOnlyDictionary<string, object> Properties { get; }

        public IIdentifier TypeId
        {
            get
            {
                if (!Properties.TryGetValue(
                    "$typeId",
                    out var rawValue))
                {
                    return null;
                }

                var value = new StringIdentifier(Convert.ToString(
                    rawValue,
                    CultureInfo.InvariantCulture));
                return value;
            }
        }

        public IIdentifier TemplateId
        {
            get
            {
                if (!Properties.TryGetValue(
                    "$templateId",
                    out var rawValue))
                {
                    return null;
                }

                var value = new StringIdentifier(Convert.ToString(
                    rawValue,
                    CultureInfo.InvariantCulture));
                return value;
            }
        }
    }
}
