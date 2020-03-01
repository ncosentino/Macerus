using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace Macerus.Plugins.Content.Wip.Items
{
    public sealed class IconGeneratorComponent : IGeneratorComponent
    {
        public IconGeneratorComponent(string iconResource)
        {
            IconResource = iconResource;
        }

        public IEnumerable<IGeneratorAttribute> SupportedAttributes { get; } = Enumerable.Empty<IGeneratorAttribute>();

        public string IconResource { get; }
    }
}
