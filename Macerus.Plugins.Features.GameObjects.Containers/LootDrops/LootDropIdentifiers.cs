using Macerus.Plugins.Features.GameObjects.Containers.Api.LootDrops;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Containers.LootDrops
{
    public sealed class LootDropIdentifiers : ILootDropIdentifiers
    {
        public IIdentifier LootDropTemplateId { get; } = new StringIdentifier("LootDrop");
    }
}
