using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Features.GameObjects.Skills.Default
{
    public sealed class SkillIconGeneratorComponent : IGeneratorComponent
    {
        public SkillIconGeneratorComponent(string iconResource)
        {
            IconResource = iconResource;
        }

        public string IconResource { get; }
    }
}
