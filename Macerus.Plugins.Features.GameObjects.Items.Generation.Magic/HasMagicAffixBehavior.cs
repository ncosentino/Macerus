
using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Magic
{
    public sealed class HasMagicAffixBehavior : 
        BaseBehavior,
        IHasMagicAffixBehavior
    {
        public HasMagicAffixBehavior(
            IIdentifier prefixStringResourceId, 
            IIdentifier suffixStringResourceId)
        {
            PrefixStringResourceId = prefixStringResourceId;
            SuffixStringResourceId = suffixStringResourceId;
        }

        public IIdentifier PrefixStringResourceId { get; }

        public IIdentifier SuffixStringResourceId { get; }
    }
}
