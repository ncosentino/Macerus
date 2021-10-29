using Macerus.Plugins.Features.GameObjects.Items.Behaviors;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Rare
{
    public interface IHasRareInventoryDisplayName : IHasInventoryDisplayName
    {
        IIdentifier PrefixStringResourceId { get; }

        IIdentifier SuffixStringResourceId { get; }
    }
}
