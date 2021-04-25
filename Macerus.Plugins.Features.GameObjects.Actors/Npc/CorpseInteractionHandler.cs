﻿using System;
using System.Linq;

using Macerus.Plugins.Features.Combat.Api;
using Macerus.Plugins.Features.GameObjects.Actors.Api;
using Macerus.Plugins.Features.GameObjects.Items.Generation.Api;
using Macerus.Plugins.Features.Interactions.Api;

using NexusLabs.Contracts;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Mapping.Api;

namespace Macerus.Plugins.Features.GameObjects.Actors.Npc
{
    public sealed class CorpseInteractionHandler : IDiscoverableInteractionHandler
    {
        private readonly IMacerusActorIdentifiers _macerusActorIdentifiers;
        private readonly ICombatStatIdentifiers _combatStatIdentifiers;
        private readonly IMapGameObjectManager _mapGameObjectManager;
        private readonly ILootGeneratorAmenity _lootGeneratorAmenity;

        public CorpseInteractionHandler(
            IMacerusActorIdentifiers macerusActorIdentifiers,
            ICombatStatIdentifiers combatStatIdentifiers)
        {
            _macerusActorIdentifiers = macerusActorIdentifiers;
            _combatStatIdentifiers = combatStatIdentifiers;
        }

        public Type InteractableType { get; } = typeof(CorpseInteractableBehavior);

        public void Interact(
            IGameObject actor,
            IInteractableBehavior behavior)
        {
            // FIXME: whyyyyy this terrible casting
            var interactableObject = (IGameObject)behavior.Owner;

            var targetStats = interactableObject.GetOnly<IHasStatsBehavior>();
            if (targetStats.BaseStats[_combatStatIdentifiers.CurrentLifeStatId] > 0)
            {
                return;
            }

            var actorInventory = actor
                .Get<IItemContainerBehavior>()
                .SingleOrDefault(x => x.ContainerId.Equals(_macerusActorIdentifiers.InventoryIdentifier));
            Contract.RequiresNotNull(
                actorInventory,
                $"'{actor}' did not have a matching '{typeof(IItemContainerBehavior)}'.");

            var sourceItemContainers = interactableObject.Get<IItemContainerBehavior>();

            foreach (var sourceItemContainer in sourceItemContainers)
            {
                var cloneItemSource = sourceItemContainer.Items.ToArray();
                foreach (var item in cloneItemSource)
                {
                    sourceItemContainer.TryRemoveItem(item);
                    actorInventory.TryAddItem(item);
                }
            }

            var sourceEquipmentBehavior = interactableObject
                .Get<ICanEquipBehavior>()
                .SingleOrDefault();
            var sourceEquipment = (sourceEquipmentBehavior
                ?.GetEquippedItemsBySlot()
                ?? Enumerable.Empty<Tuple<IIdentifier, IGameObject>>())
                .ToArray(); // NOTE: we want to take a copy, so we need to-array
            foreach (var itemAtSlot in sourceEquipment)
            {
                if (sourceEquipmentBehavior.TryUnequip(itemAtSlot.Item1, out _))
                {
                    actorInventory.TryAddItem(itemAtSlot.Item2);
                }
            }
        }
    }
}