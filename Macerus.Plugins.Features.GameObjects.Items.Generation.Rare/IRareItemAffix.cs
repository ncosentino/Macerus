using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Rare
{
    public interface IRareItemAffix : IHasFilterAttributes
    {
        IIdentifier StringResourceId { get; }
    }
}
