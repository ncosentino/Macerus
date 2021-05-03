using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.GameObjects.Skills.Api
{
    public interface IHasSkillDisplayName : IBehavior
    {
        string DisplayName { get; }
    }
}
