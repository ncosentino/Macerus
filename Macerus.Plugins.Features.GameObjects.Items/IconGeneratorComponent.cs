using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Features.GameObjects.Items
{
    public sealed class IconGeneratorComponent : IGeneratorComponent
    {
        public IconGeneratorComponent(string iconResource)
        {
            IconResource = iconResource;
        }

        public string IconResource { get; }
    }
}
