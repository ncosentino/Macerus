using System;
using System.Globalization;

using Macerus.Plugins.Features.GameObjects.Static.Api;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.Encounters.Triggers.GamObjects.Static
{
    // FIXME: these properties should all be one-time lazy loaded!
    public sealed class EncounterTriggerPropertiesBehavior :
        BaseBehavior,
        IReadOnlyEncounterTriggerPropertiesBehavior
    {
        public EncounterTriggerPropertiesBehavior(IReadOnlyStaticGameObjectPropertiesBehavior properties)
        {
            RawProperties = properties;
        }

        public IReadOnlyStaticGameObjectPropertiesBehavior RawProperties { get; }

        public double X => Convert.ToDouble(RawProperties.Properties["X"], CultureInfo.InvariantCulture);

        public double Y => Convert.ToDouble(RawProperties.Properties["Y"], CultureInfo.InvariantCulture);

        public double Width => Convert.ToDouble(RawProperties.Properties["Width"], CultureInfo.InvariantCulture);

        public double Height => Convert.ToDouble(RawProperties.Properties["Height"], CultureInfo.InvariantCulture);

        public bool MustBeMoving => RawProperties
            .Properties
            .TryGetValue(
                "MustBeMoving",
                out var rawValue) == true &&
            Convert.ToBoolean(
                rawValue,
                CultureInfo.InvariantCulture);

        public IIdentifier EncounterId
        {
            get
            {
                if (!RawProperties.Properties.TryGetValue(
                    "EncounterId",
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

        public IInterval EncounterInterval
        {
            get
            {
                if (!RawProperties.Properties.TryGetValue(
                    "EncounterInterval",
                    out var rawValue))
                {
                    return null;
                }

                if (!double.TryParse(
                    Convert.ToString(rawValue, CultureInfo.InvariantCulture),
                    NumberStyles.Any,
                    CultureInfo.InvariantCulture,
                    out var encounterInterval))
                {
                    throw new InvalidOperationException(
                        $"Could not parse encounter interval '{rawValue}'.");
                }

                var value = new Interval<double>(encounterInterval);
                return value;
            }
        }

        public double EncounterChance
        {
            get
            {
                if (!RawProperties.Properties.TryGetValue(
                    "EncounterChance",
                    out var rawValue))
                {
                    throw new InvalidOperationException(
                        $"Missing required 'EncounterChance' in '{this}'.");
                }

                if (!double.TryParse(
                    Convert.ToString(rawValue, CultureInfo.InvariantCulture),
                    NumberStyles.Any,
                    CultureInfo.InvariantCulture,
                    out var parsedValue))
                {
                    throw new InvalidOperationException(
                        $"Could not parse encounter chance '{rawValue}'.");
                }

                return parsedValue;
            }
        }
    }
}
