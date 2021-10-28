using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Features.GameObjects.Items
{
    public sealed class IconGeneratorComponent : IGeneratorComponent
    {
        public IconGeneratorComponent(IIdentifier iconResourceId)
        {
            IconResourceId = iconResourceId;
        }

        public IIdentifier IconResourceId { get; }
    }
}
