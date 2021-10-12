
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Magic
{
    public interface IHasMagicAffixBehavior : IBehavior
    {
        IIdentifier PrefixStringResourceId { get; }

        IIdentifier SuffixStringResourceId { get; }
    }
}
