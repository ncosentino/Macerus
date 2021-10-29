
using Macerus.Plugins.Features.GameObjects.Items.Behaviors;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Magic
{
    public interface IHasMagicInventoryDisplayName : IHasInventoryDisplayName
    {
        IIdentifier PrefixStringResourceId { get; }
        
        IIdentifier SuffixStringResourceId { get; }
    }
}
