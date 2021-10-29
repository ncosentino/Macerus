using System.Diagnostics;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Magic
{
    [DebuggerDisplay("{DisplayName}")]
    public sealed class HasMagicInventoryDisplayName :
        BaseBehavior,
        IHasMagicInventoryDisplayName
    {
        public HasMagicInventoryDisplayName(
            IIdentifier prefixStringResourceId,
            IIdentifier suffixStringResourceId,
            IIdentifier baseItemStringResourceId,
            string displayName)
        {
            PrefixStringResourceId = prefixStringResourceId;
            SuffixStringResourceId = suffixStringResourceId;
            BaseItemStringResourceId = baseItemStringResourceId;
            DisplayName = displayName;
        }

        public IIdentifier PrefixStringResourceId { get; }

        public IIdentifier SuffixStringResourceId { get; }
        
        public IIdentifier BaseItemStringResourceId { get; }
        
        public string DisplayName { get; }
    }
}
