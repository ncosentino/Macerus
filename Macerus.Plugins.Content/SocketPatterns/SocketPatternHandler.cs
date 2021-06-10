using System.Linq;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Filtering.Default;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes; // FIXME: dependency on non-API
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.SocketPatterns.Api;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Content.SocketPatterns
{
    public sealed class SocketPatternHandler : IDiscoverableSocketPatternHandler
    {
        private readonly IItemGeneratorFacade _itemGenerator;

        public SocketPatternHandler(
            IItemGeneratorFacade itemGenerator)
        {
            _itemGenerator = itemGenerator;
        }

        public bool TryHandle(
            IFilterContext filterContext,
            ISocketableInfo socketableInfo,
            out IGameObject newItem)
        {
            // FIXME: this is just a hack to test things out
            if (socketableInfo.OccupiedSockets.Count == 1)
            {
                var newItemFilterContext = filterContext
                    .WithAdditionalAttributes(new[]
                    {
                        new FilterAttribute(
                            new StringIdentifier("affix-type"),
                            new StringFilterAttributeValue("unique"),
                            true)
                    })
                    .WithRange(1, 1);
                newItem = _itemGenerator
                    .GenerateItems(newItemFilterContext)
                    .Single();
                return true;
            }

            newItem = null;
            return false;
        }
    }
}
