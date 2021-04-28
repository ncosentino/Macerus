using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.GameObjects.Skills.Api
{
    public interface IHasSkillIcon : IBehavior
    {
        IIdentifier IconResourceId { get; }
    }
}
