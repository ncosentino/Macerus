using System;
using System.Globalization;
using System.Linq;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.GameObjects.Containers.Api;

using NexusLabs.Contracts;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Containers
{
    public sealed class ContainerInteractableBehavior : 
        BaseBehavior,
        IInteractableBehavior
    {
        private readonly IMutableGameObjectManager _gameObjectManager;

        public ContainerInteractableBehavior(
            IMutableGameObjectManager gameObjectManager,
            bool automaticInteraction)
        {
            _gameObjectManager = gameObjectManager;
            AutomaticInteraction = automaticInteraction;
        }

        public delegate ContainerInteractableBehavior Factory(bool automaticInteraction);

        public bool AutomaticInteraction { get; }

        public void Interact(IGameObject actor)
        {
            var actorInventory = actor
                .Get<IItemContainerBehavior>()
                .SingleOrDefault(x => x.ContainerId.Equals(new StringIdentifier("Inventory")));
            Contract.RequiresNotNull(
                actorInventory,
                $"'{actor}' did not have a matching '{typeof(IItemContainerBehavior)}'.");

            var sourceItemContainer = Owner.GetOnly<IItemContainerBehavior>();
            Contract.RequiresNotNull(
                actorInventory,
                $"'{Owner}' did not have a single '{typeof(IItemContainerBehavior)}'.");

            // need a copy so we can iterate + remove
            var itemsToTake = sourceItemContainer
                .Items
                .ToArray();
            foreach (var item in itemsToTake)
            {
                sourceItemContainer.TryRemoveItem(item);
                actorInventory.TryAddItem(item);
            }

            if (Owner.GetOnly<IReadOnlyContainerPropertiesBehavior>().DestroyOnUse)
            {
                _gameObjectManager.MarkForRemoval((IGameObject)Owner); // FIXME: whyyyyy this terrible casting
            }
        }
    }
}
