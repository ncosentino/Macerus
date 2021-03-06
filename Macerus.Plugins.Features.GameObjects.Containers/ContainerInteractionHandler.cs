﻿using System;
using System.Linq;
using System.Threading.Tasks;

using Macerus.Plugins.Features.GameObjects.Actors;
using Macerus.Plugins.Features.GameObjects.Containers.Api;
using Macerus.Plugins.Features.GameObjects.Items.Generation.Api;
using Macerus.Plugins.Features.Interactions.Api;

using NexusLabs.Contracts;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Mapping;

namespace Macerus.Plugins.Features.GameObjects.Containers
{
    public sealed class ContainerInteractionHandler : IDiscoverableInteractionHandler
    {
        private readonly IMacerusActorIdentifiers _macerusActorIdentifiers;
        private readonly IMapGameObjectManager _mapGameObjectManager;
        private readonly ILootGeneratorAmenity _lootGeneratorAmenity;

        public ContainerInteractionHandler(
            IMacerusActorIdentifiers macerusActorIdentifiers,
            IMapGameObjectManager mapGameObjectManager,
            ILootGeneratorAmenity lootGeneratorAmenity)
        {
            _macerusActorIdentifiers = macerusActorIdentifiers;
            _mapGameObjectManager = mapGameObjectManager;
            _lootGeneratorAmenity = lootGeneratorAmenity;
        }

        public Type InteractableType { get; } = typeof(ContainerInteractableBehavior);

        public async Task InteractAsync(
            IGameObject actor,
            IInteractableBehavior behavior)
        {
            var interactableBehavior = (IContainerInteractableBehavior)behavior;
            var interactableObject = behavior.Owner;

            var actorInventory = actor
                .Get<IItemContainerBehavior>()
                .SingleOrDefault(x => x.ContainerId.Equals(_macerusActorIdentifiers.InventoryIdentifier));
            Contract.RequiresNotNull(
                actorInventory,
                $"'{actor}' did not have a matching '{typeof(IItemContainerBehavior)}'.");

            var sourceItemContainer = interactableObject.GetOnly<IItemContainerBehavior>();
            Contract.RequiresNotNull(
                sourceItemContainer,
                $"'{interactableObject}' did not have a single '{typeof(IItemContainerBehavior)}'.");

            GenerateNecessaryLoot(
                interactableObject,
                sourceItemContainer);

            // need a copy so we can iterate + remove
            if (interactableBehavior.TransferItemsOnActivate)
            {
                var itemsToTake = sourceItemContainer
                    .Items
                    .ToArray();
                foreach (var item in itemsToTake)
                {
                    sourceItemContainer.TryRemoveItem(item);
                    actorInventory.TryAddItem(item);
                }
            }

            if (interactableBehavior.DestroyOnUse)
            {
                _mapGameObjectManager.MarkForRemoval(interactableObject);
            }
        }

        private void GenerateNecessaryLoot(
            IGameObject interactableObject,
            IItemContainerBehavior sourceItemContainer)
        {
            foreach (var containerGenerateItemsBehavior in interactableObject.Get<IContainerGenerateItemsBehavior>())
            {
                if (containerGenerateItemsBehavior.HasGeneratedItems)
                {
                    continue;
                }

                var dropTableId = containerGenerateItemsBehavior.DropTableId;
                var generatedItems = _lootGeneratorAmenity.GenerateLoot(dropTableId);
                foreach (var generatedItem in generatedItems)
                {
                    if (!sourceItemContainer.TryAddItem(generatedItem))
                    {
                        throw new InvalidOperationException(
                            $"Could not add generated item '{generatedItem}' " +
                            $"to container '{sourceItemContainer}' belonging " +
                            $"to '{interactableObject}'.");
                    }
                }

                containerGenerateItemsBehavior.HasGeneratedItems = true;
            }
        }
    }
}
