using System.Diagnostics;
using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Enchantments.Generation.Magic
{
    [DebuggerDisplay("{SuffixId}")]
    public sealed class HasSuffixBehavior :
        BaseBehavior,
        IHasSuffixBehavior
    {
        public HasSuffixBehavior(IIdentifier suffixId)
        {
            SuffixId = suffixId;
        }

        public IIdentifier SuffixId { get; }
    }
}
