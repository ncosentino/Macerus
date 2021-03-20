﻿using System;
using System.Linq;

using Macerus.Api.Behaviors;
using Macerus.Api.Behaviors.Filtering;
using Macerus.Plugins.Features.GameObjects.Containers.Api;

using NexusLabs.Contracts;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default; // FIXME: dependency on non-API
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables;
using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Containers
{
    public sealed class ContainerInteractableBehavior : 
        BaseBehavior,
        IInteractableBehavior
    {
        private readonly IDropTableIdentifiers _dropTableIdentifiers;
        private readonly IMapGameObjectManager _mapGameObjectManager;
        private readonly ILootGenerator _lootGenerator;
        private readonly IFilterContextProvider _filterContextProvider;
        private readonly IFilterContextAmenity _filterContextAmenity;
        private readonly IDropTableRepositoryFacade _dropTableRepository;

        public ContainerInteractableBehavior(
            IDropTableIdentifiers dropTableIdentifiers,
            IMapGameObjectManager mapGameObjectManager,
            ILootGenerator lootGenerator,
            IFilterContextProvider filterContextProvider,
            IFilterContextAmenity filterContextAmenity,
            IDropTableRepositoryFacade dropTableRepository,
            bool automaticInteraction)
        {
            _dropTableIdentifiers = dropTableIdentifiers;
            _mapGameObjectManager = mapGameObjectManager;
            _lootGenerator = lootGenerator;
            _filterContextProvider = filterContextProvider;
            _filterContextAmenity = filterContextAmenity;
            _dropTableRepository = dropTableRepository;
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
            IFilterContext baseGeneratorContext = null;
            foreach (var containerGenerateItemsBehavior in Owner.Get<IContainerGenerateItemsBehavior>())
            {
                if (containerGenerateItemsBehavior.HasGeneratedItems)
                {
                    continue;
                }

                if (baseGeneratorContext == null)
                {
                    baseGeneratorContext = _filterContextProvider.GetContext();
                }

                var dropTableId = containerGenerateItemsBehavior.DropTableId;
                var dropTable = _dropTableRepository.GetForDropTableId(dropTableId);
                var dropTableAttribute = _filterContextAmenity.CreateRequiredAttribute(
                    _dropTableIdentifiers.FilterContextDropTableIdentifier,
                    dropTableId);

                var filterContext = baseGeneratorContext
                    .WithAdditionalAttributes(new[] { dropTableAttribute })
                    .WithRange(
                        dropTable.MinimumGenerateCount,
                        dropTable.MaximumGenerateCount);

                var generatedItems = _lootGenerator.GenerateLoot(filterContext);
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
