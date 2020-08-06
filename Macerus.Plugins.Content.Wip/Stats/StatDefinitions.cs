using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Content.Wip.Stats
{
    public static class StatDefinitions
    {
        private static Lazy<IReadOnlyCollection<IIdentifier>> _lazyAllStats = new Lazy<IReadOnlyCollection<IIdentifier>>(() =>
            {
                return typeof(StatDefinitions)
                    .GetProperties(BindingFlags.Static | BindingFlags.Public)
                    .Where(p => p.PropertyType == typeof(IIdentifier))
                    .Select(p => p.GetValue(null))
                    .Cast<IIdentifier>()
                    .ToArray();
            });

        public static IReadOnlyCollection<IIdentifier> All => _lazyAllStats.Value;

        public static IIdentifier MaximumLife { get; } = new StringIdentifier("Maximum Life");

        public static IIdentifier CurrentLife { get; } = new StringIdentifier("Current Life");

        public static IIdentifier MaximumMana { get; } = new StringIdentifier("Maximum Mana");

        public static IIdentifier CurrentMana { get; } = new StringIdentifier("Current Mana");

        public static IIdentifier LightRadiusRadius { get; } = new StringIdentifier("Light Radius Radius");

        public static IIdentifier LightRadiusIntensity { get; } = new StringIdentifier("Light Radius Intensity");

        public static IIdentifier LightRadiusRed { get; } = new StringIdentifier("Light Radius Red");

        public static IIdentifier LightRadiusGreen { get; } = new StringIdentifier("Light Radius Green");

        public static IIdentifier LightRadiusBlue { get; } = new StringIdentifier("Light Radius Blue");
    }
}
