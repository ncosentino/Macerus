using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.SocketPatterns.Api;
using ProjectXyz.Shared.Behaviors.Filtering;
using ProjectXyz.Shared.Behaviors.Filtering.Attributes;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Items.Socketing.SocketPatterns
{
    public sealed class SocketPatternHandler : IDiscoverableSocketPatternHandler
    {
        private readonly IItemGeneratorFacade _itemGenerator;
        private readonly IFilterContextProvider _filterContextProvider;

        public SocketPatternHandler(
            IItemGeneratorFacade itemGenerator,
            IFilterContextProvider filterContextProvider)
        {
            _itemGenerator = itemGenerator;
            _filterContextProvider = filterContextProvider;
        }

        public bool TryHandle(
            ISocketableInfo socketableInfo,
            out IGameObject newItem)
        {
            // FIXME: this is just a hack to test things out
            if (socketableInfo.OccupiedSockets.Count == 1)
            {
                var filterContext = _filterContextProvider
                    .GetContext()
                    .WithAdditionalAttributes(new[]
                    {
                        new FilterAttribute(
                            new StringIdentifier("affix-type"),
                            new StringFilterAttributeValue("unique"),
                            true)
                    })
                    .WithRange(1, 1);
                newItem = _itemGenerator
                    .GenerateItems(filterContext)
                    .Single();
                return true;
            }

            newItem = null;
            return false;
        }
    }
}
