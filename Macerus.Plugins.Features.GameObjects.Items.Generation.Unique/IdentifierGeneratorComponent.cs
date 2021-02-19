using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace Macerus.Plugins.Content.Wip.Items
{
    public sealed class UniqueBaseItemGeneratorComponent : IGeneratorComponent
    {
        public UniqueBaseItemGeneratorComponent(IIdentifier identifier)
        {
            Identifier = identifier;
        }

        public IEnumerable<IGeneratorAttribute> SupportedAttributes { get; } = Enumerable.Empty<IGeneratorAttribute>();
        
        public IIdentifier Identifier { get; }
    }
}
