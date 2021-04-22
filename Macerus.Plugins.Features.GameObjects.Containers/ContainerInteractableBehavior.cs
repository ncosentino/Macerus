using System;
using System.Linq;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.GameObjects.Actors.Api;
using Macerus.Plugins.Features.GameObjects.Containers.Api;
using Macerus.Plugins.Features.GameObjects.Items.Generation.Api;

using NexusLabs.Contracts;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Containers
{
    public sealed class ContainerInteractableBehavior : 
        BaseBehavior,
        IInteractableBehavior
    {
        private readonly IMacerusActorIdentifiers _macerusActorIdentifiers;
        private readonly IMapGameObjectManager _mapGameObjectManager;
        private readonly ILootGeneratorAmenity _lootGeneratorAmenity;

        public ContainerInteractableBehavior(
            IMacerusActorIdentifiers macerusActorIdentifiers,
            IMapGameObjectManager mapGameObjectManager,
            ILootGeneratorAmenity lootGeneratorAmenity,
            bool automaticInteraction)
        {
            _macerusActorIdentifiers = macerusActorIdentifiers;
            _mapGameObjectManager = mapGameObjectManager;
            _lootGeneratorAmenity = lootGeneratorAmenity;
            AutomaticInteraction = automaticInteraction;
        }

        public delegate ContainerInteractableBehavior Factory(bool automaticInteraction);

        public bool AutomaticInteraction { get; }

        public void Interact(IGameObject actor)
        {
            var actorInventory = actor
                .Get<IItemContainerBehavior>()
                .SingleOrDefault(x => x.ContainerId.Equals(_macerusActorIdentifiers.InventoryIdentifier));
            Contract.RequiresNotNull(
                actorInventory,
                $"'{actor}' did not have a matching '{typeof(IItemContainerBehavior)}'.");

            var sourceItemContainer = Owner.GetOnly<IItemContainerBehavior>();
            Contract.RequiresNotNull(
                actorInventory,
                $"'{Owner}' did not have a single '{typeof(IItemContainerBehavior)}'.");

            var properties = Owner.GetOnly<IReadOnlyContainerPropertiesBehavior>();
            Contract.RequiresNotNull(
                actorInventory,
                $"'{Owner}' did not have a single '{typeof(IReadOnlyContainerPropertiesBehavior)}'.");

            GenerateNecessaryLoot(sourceItemContainer);

            // need a copy so we can iterate + remove
            if (properties.TransferItemsOnActivate)
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

            if (properties.DestroyOnUse)
            {
                _mapGameObjectManager.MarkForRemoval((IGameObject)Owner); // FIXME: whyyyyy this terrible casting
            }
        }

        private void GenerateNecessaryLoot(IItemContainerBehavior sourceItemContainer)
        {
            foreach (var containerGenerateItemsBehavior in Owner.Get<IContainerGenerateItemsBehavior>())
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
                            $"to '{Owner}'.");
                    }
                }

                containerGenerateItemsBehavior.HasGeneratedItems = true;
            }
        }
    }
}
