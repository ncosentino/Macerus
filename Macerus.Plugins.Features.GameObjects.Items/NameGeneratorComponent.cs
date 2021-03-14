using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Features.GameObjects.Items
{
    public sealed class NameGeneratorComponent : IGeneratorComponent
    {
        public NameGeneratorComponent(string displayName)
        {
            DisplayName = displayName;
        }

        public string DisplayName { get; }
    }
}
