using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Content.Wip.Stats
{
    public static class StatDefinitions
    {
        public static IIdentifier MaximumLife { get; } = new StringIdentifier("Maximum Life");

        public static IIdentifier CurrentLife { get; } = new StringIdentifier("Current Life");

        public static IIdentifier MaximumMana { get; } = new StringIdentifier("Maximum Mana");

        public static IIdentifier CurrentMana { get; } = new StringIdentifier("Current Mana");
    }
}
