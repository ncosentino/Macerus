using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.GameObjects.Actors.Api;
using Macerus.Plugins.Features.GameObjects.Items.Generation.Api;

using NexusLabs.Contracts;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Plugins.Features.GameObjects.Actors.Generation
{
    public sealed class SpawnWithItemsGeneratorComponentToBehaviorConverter : IDiscoverableGeneratorComponentToBehaviorConverter
    {
        private readonly IMacerusActorIdentifiers _actorIdentifiers;
        private readonly ILootGeneratorAmenity _lootGeneratorAmenity;

        public SpawnWithItemsGeneratorComponentToBehaviorConverter(
            IMacerusActorIdentifiers actorIdentifiers,
            ILootGeneratorAmenity lootGeneratorAmenity)
        {
            _actorIdentifiers = actorIdentifiers;
            _lootGeneratorAmenity = lootGeneratorAmenity;
        }

        public Type ComponentType => typeof(SpawnWithItemsGeneratorComponent);

        public IEnumerable<IBehavior> Convert(
            IFilterContext filterContext,
            IEnumerable<IBehavior> baseBehaviors,
            IGeneratorComponent generatorComponent)
        {
            var spawnWithItemsComponent = (SpawnWithItemsGeneratorComponent)generatorComponent;
            var inventoryBehavior = baseBehaviors
                .Get<IItemContainerBehavior>()
                .FirstOrDefault(x => x.ContainerId.Equals(_actorIdentifiers.InventoryIdentifier));
            Contract.RequiresNotNull(
                inventoryBehavior,
                $"Could not find an item container behavior with ID " +
                $"'{_actorIdentifiers.InventoryIdentifier}' when converting " +
                $"'{generatorComponent}'.");

            var generatedItems = _lootGeneratorAmenity.GenerateLoot(spawnWithItemsComponent.DropTableId);
            foreach (var generatedItem in generatedItems)
            {
                if (!inventoryBehavior.TryAddItem(generatedItem))
                {
                    throw new InvalidOperationException(
                        $"Could not add item '{generatedItem}' to " +
                        $"'{inventoryBehavior}' when converting " +
                        $"'{generatorComponent}'.");
                }
            }

            yield break;
        }
    }
}
