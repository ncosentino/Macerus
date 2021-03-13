using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Static
{
    public sealed class StaticGameObjectIdentifiers : IStaticGameObjectIdentifiers
    {
        public IIdentifier StaticGameObjectTypeIdentifier { get; } = new StringIdentifier("static");
    }
}
