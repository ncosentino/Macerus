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
    public static class Affixes
    {
        public static class Prefixes
        {
            private static Lazy<IReadOnlyCollection<IIdentifier>> _lazyAll = new Lazy<IReadOnlyCollection<IIdentifier>>(() =>
            {
                return typeof(StatDefinitions)
                    .GetProperties(BindingFlags.Static | BindingFlags.Public)
                    .Where(p => p.PropertyType == typeof(IIdentifier))
                    .Select(p => p.GetValue(null))
                    .Cast<IIdentifier>()
                    .ToArray();
            });

            public static IReadOnlyCollection<IIdentifier> All => _lazyAll.Value;

            public static IIdentifier Lively { get; } = new StringIdentifier("Lively");

            public static IIdentifier Hearty { get; } = new StringIdentifier("Hearty");

            public static IIdentifier Magic { get; } = new StringIdentifier("Magic");
        }

        public static class Suffixes
        {
            private static Lazy<IReadOnlyCollection<IIdentifier>> _lazyAll = new Lazy<IReadOnlyCollection<IIdentifier>>(() =>
            {
                return typeof(StatDefinitions)
                    .GetProperties(BindingFlags.Static | BindingFlags.Public)
                    .Where(p => p.PropertyType == typeof(IIdentifier))
                    .Select(p => p.GetValue(null))
                    .Cast<IIdentifier>()
                    .ToArray();
            });

            public static IReadOnlyCollection<IIdentifier> All => _lazyAll.Value;

            public static IIdentifier OfLife { get; } = new StringIdentifier("of Life");

            public static IIdentifier OfHeart { get; } = new StringIdentifier("of Heart");

            public static IIdentifier OfMana { get; } = new StringIdentifier("of Mana");
        }
    }
}
