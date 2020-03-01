using System;
using System.Collections.Generic;
using System.Linq;
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
            public static IIdentifier Lively { get; } = new StringIdentifier("Lively");

            public static IIdentifier Magic { get; } = new StringIdentifier("magic");
        }

        public static class Suffixes
        {
            public static IIdentifier OfLife { get; } = new StringIdentifier("of Life");

            public static IIdentifier OfMana { get; } = new StringIdentifier("of Mana");
        }
    }
}
