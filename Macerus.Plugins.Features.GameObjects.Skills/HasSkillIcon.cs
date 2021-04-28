using Macerus.Plugins.Features.GameObjects.Skills.Api;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Skills.Default
{
    public sealed class HasSkillIcon :
        BaseBehavior,
        IHasSkillIcon
    {
        public HasSkillIcon(IIdentifier iconResourceId)
        {
            IconResourceId = iconResourceId;
        }

        public IIdentifier IconResourceId { get; }
    }
}
