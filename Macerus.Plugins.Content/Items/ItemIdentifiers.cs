using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api;

namespace Macerus.Plugins.Content.Items
{
    public sealed class ItemIdentifiers : IItemIdentifiers
    {
        public IIdentifier ItemDefinitionIdentifier { get; } = new StringIdentifier("item-id");
    }
}
