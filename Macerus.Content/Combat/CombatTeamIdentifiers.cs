using Macerus.Plugins.Features.Combat.Api;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;

namespace Macerus.Content.Combat
{
    public sealed class CombatTeamIdentifiers : ICombatTeamIdentifiers
    {
        public IIdentifier CombatTeamStatDefinitionId { get; } = new StringIdentifier("stat_67");

        public int PlayerTeamStatValue { get; } = 0;

        public int EnemyTeam1StatValue { get; } = 1;
    }
}
