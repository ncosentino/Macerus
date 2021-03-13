
using Macerus.Plugins.Features.GameObjects.Containers.Api;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Containers
{
    public sealed class ContainerIdentifiers : IContainerIdentifiers
    {
        public IIdentifier ContainerTypeIdentifier { get; } = new StringIdentifier("container");
    }
}
