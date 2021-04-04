
using Macerus.Plugins.Features.Combat.Api;

namespace Macerus.Plugins.Features.Combat.Default
{
    public sealed class CombatTeamGeneratorComponent : ICombatTeamGeneratorComponent
    {
        public CombatTeamGeneratorComponent(int team)
        {
            Team = team;
        }

        public int Team { get; set; }
    }
}
