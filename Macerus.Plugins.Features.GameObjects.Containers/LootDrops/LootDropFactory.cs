using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Api.GameObjects;
using Macerus.Plugins.Features.GameObjects.Containers.Api.LootDrops;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Macerus.Plugins.Features.GameObjects.Containers.LootDrops
{
    public sealed class LootDropFactory : ILootDropFactory
    {
        private readonly ILootDropIdentifiers _lootDropIdentifiers;
        private readonly IGameObjectRepositoryAmenity _gameObjectRepositoryAmenity;

        public LootDropFactory(
            ILootDropIdentifiers lootDropIdentifiers,
            IGameObjectRepositoryAmenity gameObjectRepositoryAmenity)
        {
            _lootDropIdentifiers = lootDropIdentifiers;
            _gameObjectRepositoryAmenity = gameObjectRepositoryAmenity;
        }

        public IGameObject CreateLoot(
            double worldX,
            double worldY,
            bool automaticInteraction,
            IGameObject item,
            params IGameObject[] items) =>
            CreateLoot(
                worldX,
                worldY,
                automaticInteraction,
                item.Yield().Concat(items ?? new IGameObject[0]));

        public IGameObject CreateLoot(
            double worldX,
            double worldY,
            bool automaticInteraction,
            IEnumerable<IGameObject> items)
        {
            var lootObject = _gameObjectRepositoryAmenity.CreateGameObjectFromTemplate(
                _lootDropIdentifiers.LootDropTemplateId,
                new IBehavior[]
                {
                    new ContainerInteractableBehavior(automaticInteraction, true, true),
                    new PositionBehavior(worldX, worldY),
                });

            var itemContainerBehavior = lootObject.GetOnly<IItemContainerBehavior>();
            foreach (var item in items)
            {
                if (!itemContainerBehavior.TryAddItem(item))
                {
                    throw new InvalidOperationException(
                        $"Could not add '{item}' to '{itemContainerBehavior}' " +
                        $"of '{lootObject}' when creating loot.");
                }
            }

            return lootObject;
        }
    }
}
