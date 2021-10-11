using System.Diagnostics;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Magic.Enchantments
{
    [DebuggerDisplay("{PrefixStringResourceId}")]
    public sealed class HasPrefixBehavior :
        BaseBehavior,
        IHasPrefixBehavior
    {
        public HasPrefixBehavior(IIdentifier prefixStringResourceId)
        {
            PrefixStringResourceId = prefixStringResourceId;
        }

        public IIdentifier PrefixStringResourceId { get; }
    }
}
