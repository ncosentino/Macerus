using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace Macerus.Plugins.Content.Wip.Items
{
    public sealed class NameGeneratorComponent : IGeneratorComponent
    {
        public NameGeneratorComponent(string displayName)
        {
            DisplayName = displayName;
        }

        public IEnumerable<IGeneratorAttribute> SupportedAttributes { get; } = Enumerable.Empty<IGeneratorAttribute>();

        public string DisplayName { get; }
    }
}
