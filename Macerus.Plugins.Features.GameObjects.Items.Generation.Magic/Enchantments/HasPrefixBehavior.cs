using System.Diagnostics;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Magic.Enchantments
{
    [DebuggerDisplay("{PrefixId}")]
    public sealed class HasPrefixBehavior :
        BaseBehavior,
        IHasPrefixBehavior
    {
        public HasPrefixBehavior(IIdentifier prefixId)
        {
            PrefixId = prefixId;
        }

        public IIdentifier PrefixId { get; }
    }
}
