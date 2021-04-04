using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Combat.Api
{
    public interface ICombatTeamIdentifiers
    {
        IIdentifier CombatTeamStatDefinitionId { get; }

        int PlayerTeamStatValue { get; }

        int EnemyTeam1StatValue { get; }
    }
}
