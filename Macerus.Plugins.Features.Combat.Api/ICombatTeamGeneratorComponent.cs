using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Features.Combat.Api
{
    public interface ICombatTeamGeneratorComponent : IGeneratorComponent
    {
        int Team { get; set; }
    }
}
