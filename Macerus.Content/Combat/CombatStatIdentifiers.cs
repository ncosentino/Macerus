using Macerus.Plugins.Features.Combat.Api;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;

namespace Macerus.Content.Combat
{
    public sealed class CombatStatIdentifiers : ICombatStatIdentifiers
    {
        public IIdentifier CurrentLifeStatId { get; } = new StringIdentifier("stat_2");

        public IIdentifier TurnSpeedStatId { get; } = new StringIdentifier("stat_80");

        public IIdentifier AttackSpeedStatId { get; } = new StringIdentifier("stat_64");

        public IIdentifier FireDamageMinStatId { get; } = new StringIdentifier("stat_15");

        public IIdentifier FireDamageMaxStatId { get; } = new StringIdentifier("stat_25");

        public IIdentifier FireResistStatId { get; } = new StringIdentifier("stat_35");

        public IIdentifier PhysicalDamageMinStatId { get; } = new StringIdentifier("stat_19");

        public IIdentifier PhysicalDamageMaxStatId { get; } = new StringIdentifier("stat_29");

        public IIdentifier PhysicalResistStatId { get; } = new StringIdentifier("stat_49");
    }
}
