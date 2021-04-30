using ProjectXyz.Shared.Game.Behaviors;

using Macerus.Plugins.Features.GameObjects.Skills.Api;

namespace Macerus.Plugins.Features.GameObjects.Skills.Default
{
    public sealed class HasSkillDisplayName :
        BaseBehavior,
        IHasSkillDisplayName
    {
        public HasSkillDisplayName(string displayName)
        {
            DisplayName = displayName;
        }

        public string DisplayName { get; }
    }
}
