using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Combat.Api
{
    public interface ICombatStatIdentifiers
    {
        IIdentifier CurrentLifeStatId { get; }

        IIdentifier TurnSpeedStatId { get; }

        IIdentifier AttackSpeedStatId { get; }

        IIdentifier FireDamageMinStatId { get; }

        IIdentifier FireDamageMaxStatId { get; }

        IIdentifier FireResistStatId { get; }

        IIdentifier PhysicalDamageMinStatId { get; }

        IIdentifier PhysicalDamageMaxStatId { get; }

        IIdentifier PhysicalResistStatId { get; }
    }
}
