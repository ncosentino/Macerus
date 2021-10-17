using Macerus.Plugins.Features.Combat.Api;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;

namespace Macerus.Content.Combat
{
    public sealed class CombatStatIdentifiers : ICombatStatIdentifiers
    {
        public IIdentifier CurrentLifeStatId { get; } = new IntIdentifier(2);

        public IIdentifier TurnSpeedStatId { get; } = new IntIdentifier(79);

        public IIdentifier AttackSpeedStatId { get; } = new IntIdentifier(63);

        public IIdentifier FireDamageMinStatId { get; } = new IntIdentifier(14);

        public IIdentifier FireDamageMaxStatId { get; } = new IntIdentifier(24);

        public IIdentifier FireResistStatId { get; } = new IntIdentifier(34);

        public IIdentifier PhysicalDamageMinStatId { get; } = new IntIdentifier(18);

        public IIdentifier PhysicalDamageMaxStatId { get; } = new IntIdentifier(28);

        public IIdentifier PhysicalResistStatId { get; } = new IntIdentifier(38);
    }
}
