using System.Diagnostics;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Magic.Enchantments
{
    [DebuggerDisplay("{SuffixStringResourceId}")]
    public sealed class HasSuffixBehavior :
        BaseBehavior,
        IHasSuffixBehavior
    {
        public HasSuffixBehavior(IIdentifier suffixStringResourceId)
        {
            SuffixStringResourceId = suffixStringResourceId;
        }

        public IIdentifier SuffixStringResourceId { get; }
    }
}
