﻿using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.GameObjects.Items.Generation.Api;

using NexusLabs.Contracts;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Filtering.Api;
using Macerus.Api.Behaviors.Filtering;

namespace Macerus.Plugins.Features.GameObjects.Actors.Generation
{
    public sealed class SpawnWithEquipmentGeneratorComponentToBehaviorConverter : IDiscoverableGeneratorComponentToBehaviorConverter
    {
        private readonly ILootGeneratorAmenity _lootGeneratorAmenity;
        private readonly Lazy<IFilterContextAmenity> _lazyFilterContextAmenity;

        public SpawnWithEquipmentGeneratorComponentToBehaviorConverter(
            ILootGeneratorAmenity lootGeneratorAmenity,
            Lazy<IFilterContextAmenity> lazyFilterContextAmenity)
        {
            _lootGeneratorAmenity = lootGeneratorAmenity;
            _lazyFilterContextAmenity = lazyFilterContextAmenity;
        }

        public Type ComponentType => typeof(SpawnWithEquipmentGeneratorComponent);

        public IEnumerable<IBehavior> Convert(
            IFilterContext filterContext,
            IEnumerable<IBehavior> baseBehaviors,
            IGeneratorComponent generatorComponent)
        {
            var spawnWithEquipmentComponent = (SpawnWithEquipmentGeneratorComponent)generatorComponent;
            var equipmentBehavior = baseBehaviors
                .Get<ICanEquipBehavior>()
                .FirstOrDefault();
            Contract.RequiresNotNull(
                equipmentBehavior,
                $"Could not find a behavior of type " +
                $"'{typeof(ICanEquipBehavior)}' when converting " +
                $"'{generatorComponent}'.");

            var inventoryBehavior = spawnWithEquipmentComponent.FailedEquipInventoryId == null
                ? null
                : baseBehaviors
                    .Get<IItemContainerBehavior>()
                    .FirstOrDefault(x => x.ContainerId.Equals(spawnWithEquipmentComponent.FailedEquipInventoryId));

            var lootContext = _lazyFilterContextAmenity
                .Value
                .CreateFilterContextForAnyAmount(filterContext.Attributes.Where(x => !x.Required));
            var generatedItems = _lootGeneratorAmenity.GenerateLoot(
                spawnWithEquipmentComponent.DropTableId,
                lootContext);
            foreach (var generatedItem in generatedItems)
            {
                var canBeEquippedBehavior = generatedItem
                    .Get<ICanBeEquippedBehavior>()
                    .FirstOrDefault();
                if (canBeEquippedBehavior == null)
                {
                    if (!spawnWithEquipmentComponent.BestEffortEquip)
                    {
                        throw new InvalidOperationException(
                            $"Could not find a behavior of type " +
                            $"'{typeof(ICanBeEquippedBehavior)}' when trying to equip " +
                            $"item '{generatedItem}' during conversion of generator " +
                            $"component '{generatorComponent}'.");
                    }

                    inventoryBehavior?.TryAddItem(generatedItem);
                    continue;
                }

                // try to get the first free one that we can equip to but
                // otherwise... just assign the first one
                var slotToEquipTo = canBeEquippedBehavior
                    .AllowedEquipSlots
                    .FirstOrDefault(x => !equipmentBehavior.TryGet(x, out _));
                if (slotToEquipTo == null)
                {
                    slotToEquipTo = canBeEquippedBehavior
                        .AllowedEquipSlots
                        .First();
                }
                
                if (!equipmentBehavior.TryEquip(
                    slotToEquipTo,
                    canBeEquippedBehavior,
                    true))
                {
                    if (!spawnWithEquipmentComponent.BestEffortEquip)
                    {
                        throw new InvalidOperationException(
                            $"Could not equip item '{generatedItem}' to " +
                            $"'{equipmentBehavior}' when converting " +
                            $"'{generatorComponent}'.");
                    }

                    inventoryBehavior?.TryAddItem(generatedItem);
                    continue;
                }
            }

            yield break;
        }
    }
}
