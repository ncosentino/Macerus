using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Actors.Default.Animations
{
    public sealed class DynamicAnimationIdentifiers : IDynamicAnimationIdentifiers
    {
        public IIdentifier AnimationSpeedMultiplierStatId { get; } = new StringIdentifier("animation_speed_multiplier");

        public IIdentifier RedMultiplierStatId { get; } = new StringIdentifier("animation_red_multiplier");

        public IIdentifier GreenMultiplierStatId { get; } = new StringIdentifier("animation_green_multiplier");

        public IIdentifier BlueMultiplierStatId { get; } = new StringIdentifier("animation_blue_multiplier");

        public IIdentifier AlphaMultiplierStatId { get; } = new StringIdentifier("animation_alpha_multiplier");

        public IIdentifier AnimationOverrideStatId { get; } = new StringIdentifier("animation_override");

        public static IEnumerable<IIdentifier> GetAllStatDefinitionIds(IDynamicAnimationIdentifiers identifiers)
        {
            var properties = identifiers.GetType()
                .GetProperties(
                    BindingFlags.Public |
                    BindingFlags.Instance |
                    BindingFlags.GetProperty)
                .Where(x => typeof(IIdentifier).IsAssignableFrom(x.PropertyType))
                .ToArray();
            var values = properties
                .Select(x => (IIdentifier)x.GetValue(identifiers))
                .ToArray();
            return values;
        }
    }
}
