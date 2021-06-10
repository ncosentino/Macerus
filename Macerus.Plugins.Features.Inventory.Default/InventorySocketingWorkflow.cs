using System;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Socketing.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.SocketPatterns.Api;

namespace Macerus.Plugins.Features.Inventory.Default
{
    public sealed class InventorySocketingWorkflow : IInventorySocketingWorkflow
    {
        private readonly ISocketPatternHandlerFacade _socketPatternHandler;
        private readonly ISocketableInfoFactory _socketableInfoFactory;
        private readonly IFilterContextProvider _filterContextProvider;

        public InventorySocketingWorkflow(
            ISocketPatternHandlerFacade socketPatternHandler,
            ISocketableInfoFactory socketableInfoFactory,
            IFilterContextProvider filterContextProvider)
        {
            _socketPatternHandler = socketPatternHandler;
            _socketableInfoFactory = socketableInfoFactory;
            _filterContextProvider = filterContextProvider;
        }

        public bool TrySocketItem(
            IItemContainerBehavior itemContainerBehavior,
            IGameObject itemToBePlacedInsideSocket,
            IGameObject itemToHaveSocketFilled)
        {
            if (itemToBePlacedInsideSocket == itemToHaveSocketFilled)
            {
                return false;
            }

            if (!itemToHaveSocketFilled.TryGetFirst<ICanBeSocketedBehavior>(out var canBeSocketedBehavior))
            {
                return false;
            }

            if (!itemToBePlacedInsideSocket.TryGetFirst<ICanFitSocketBehavior>(out var canFitSocketBehavior))
            {
                return false;
            }

            if (!canBeSocketedBehavior.CanFitSocket(canFitSocketBehavior))
            {
                return false;
            }

            if (!canBeSocketedBehavior.Socket(canFitSocketBehavior))
            {
                throw new InvalidOperationException(
                    $"Check to socket '{canFitSocketBehavior}' into " +
                    $"'{canBeSocketedBehavior}' passed, but " +
                    $"{nameof(ICanBeSocketedBehavior.Socket)}() was not " +
                    $"successful.");
            }

            if (!itemContainerBehavior.TryRemoveItem(itemToBePlacedInsideSocket))
            {
                throw new InvalidOperationException(
                    $"'{canFitSocketBehavior}' was socketed into " +
                    $"'{canBeSocketedBehavior}', but could not remove " +
                    $"'{itemToBePlacedInsideSocket}' from the source container.");
            }

            var filterContext = _filterContextProvider.GetContext();
            if (_socketPatternHandler.TryHandle(
                filterContext,
                _socketableInfoFactory.Create(
                    canBeSocketedBehavior.Owner,
                    canBeSocketedBehavior),
                out var newSocketPatternItem))
            {
                if (!itemContainerBehavior.TryRemoveItem(itemToHaveSocketFilled))
                {
                    throw new InvalidOperationException(
                        $"'{canFitSocketBehavior}' was socketed into " +
                        $"'{canBeSocketedBehavior}' and created socket pattern " +
                        $"item '{newSocketPatternItem}'. However, the original " +
                        $"'{itemToHaveSocketFilled}' could not be removed from " +
                        $"'{itemContainerBehavior}'.");
                }

                if (!itemContainerBehavior.TryAddItem(newSocketPatternItem))
                {
                    throw new InvalidOperationException(
                        $"A socket pattern item '{newSocketPatternItem}' was " +
                        $"created, but could not be added to " +
                        $"'{itemContainerBehavior}'.");
                }
            }

            return true;
        }
    }
}
