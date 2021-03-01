using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.Weather
{
    public static class WeatherIds
    {
        private static readonly Lazy<IReadOnlyCollection<IIdentifier>> _allLazy = 
            new Lazy<IReadOnlyCollection<IIdentifier>>(() =>
            {
                var properties = typeof(WeatherIds)
                    .GetProperties(
                        BindingFlags.Public |
                        BindingFlags.Static |
                        BindingFlags.GetProperty)
                    .Where(x => typeof(IIdentifier).IsAssignableFrom(x.PropertyType))
                    .ToArray();
                var values = properties
                    .Select(x => (IIdentifier)x.GetValue(null))
                    .ToArray();
                return values;
            });

        public static IReadOnlyCollection<IIdentifier> All => _allLazy.Value;

        public static IIdentifier Clear { get; } = new StringIdentifier("clear");

        public static IIdentifier Rain { get; } = new StringIdentifier("rain");

        public static IIdentifier Snow { get; } = new StringIdentifier("snow");

        public static IIdentifier Overcast { get; } = new StringIdentifier("overcast");
    }
}
