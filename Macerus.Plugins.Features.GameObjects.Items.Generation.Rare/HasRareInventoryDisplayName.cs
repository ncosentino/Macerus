using System.Diagnostics;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Rare
{
    [DebuggerDisplay("{DisplayName}")]
    public sealed class HasRareInventoryDisplayName :
        BaseBehavior,
        IHasRareInventoryDisplayName
    {
        public HasRareInventoryDisplayName(
            IIdentifier prefixStringResourceId,
            IIdentifier suffixStringResourceId,
            string displayName)
        {
            PrefixStringResourceId = prefixStringResourceId;
            SuffixStringResourceId = suffixStringResourceId;
            DisplayName = displayName;
        }

        public IIdentifier PrefixStringResourceId { get; }

        public IIdentifier SuffixStringResourceId { get; }

        public string DisplayName { get; }
    }
}
