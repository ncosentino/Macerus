using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Content.Enchantments
{
    public static class Affixes
    {
        public static class Prefixes
        {
            private static Lazy<IReadOnlyCollection<IIdentifier>> _lazyAll = new Lazy<IReadOnlyCollection<IIdentifier>>(() =>
            {
                return typeof(Prefixes)
                    .GetProperties(BindingFlags.Static | BindingFlags.Public)
                    .Where(p => p.PropertyType == typeof(IIdentifier))
                    .Select(p => p.GetValue(null))
                    .Cast<IIdentifier>()
                    .ToArray();
            });

            public static IReadOnlyCollection<IIdentifier> All => _lazyAll.Value;

            public static IIdentifier Lively { get; } = new IntIdentifier(1);

            public static IIdentifier Hearty { get; } = new IntIdentifier(2);

            public static IIdentifier Magic { get; } = new IntIdentifier(3);
        }

        public static class Suffixes
        {
            private static Lazy<IReadOnlyCollection<IIdentifier>> _lazyAll = new Lazy<IReadOnlyCollection<IIdentifier>>(() =>
            {
                return typeof(Suffixes)
                    .GetProperties(BindingFlags.Static | BindingFlags.Public)
                    .Where(p => p.PropertyType == typeof(IIdentifier))
                    .Select(p => p.GetValue(null))
                    .Cast<IIdentifier>()
                    .ToArray();
            });

            public static IReadOnlyCollection<IIdentifier> All => _lazyAll.Value;

            public static IIdentifier OfLife { get; } = new IntIdentifier(4);

            public static IIdentifier OfHeart { get; } = new IntIdentifier(5);

            public static IIdentifier OfMana { get; } = new IntIdentifier(6);
        }
    }
}
