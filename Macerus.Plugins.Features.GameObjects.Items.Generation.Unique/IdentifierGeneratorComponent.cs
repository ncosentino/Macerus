using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Unique
{
    public sealed class UniqueBaseItemGeneratorComponent : IGeneratorComponent
    {
        public UniqueBaseItemGeneratorComponent(IIdentifier identifier)
        {
            Identifier = identifier;
        }

        public IIdentifier Identifier { get; }
    }
}
