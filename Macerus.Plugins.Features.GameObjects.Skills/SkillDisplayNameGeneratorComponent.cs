using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Features.GameObjects.Skills.Default
{
    public sealed class SkillDisplayNameGeneratorComponent : IGeneratorComponent
    {
        public SkillDisplayNameGeneratorComponent(string displayName)
        {
            DisplayName = displayName;
        }

        public string DisplayName { get; }
    }
}
