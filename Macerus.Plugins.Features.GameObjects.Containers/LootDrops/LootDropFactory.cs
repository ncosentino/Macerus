using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.GameObjects.Containers.Api;
using Macerus.Plugins.Features.GameObjects.Containers.Api.LootDrops;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Containers.LootDrops
{
    public sealed class LootDropFactory : ILootDropFactory
    {
        private readonly IContainerRepository _containerRepository;

        public LootDropFactory(IContainerRepository containerRepository)
        {
            _containerRepository = containerRepository;
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
            var lootObject = _containerRepository.CreateFromTemplate(
                // note: if this seems redundant, it's because repos can handle 
                // multiple types if they choose... don't forget it! :)
                ContainerRepository.ContainerTypeId,
                new StringIdentifier("LootDrop"),
                new Dictionary<string, object>()
                {
                    ["X"] = worldX,
                    ["Y"] = worldY,
                    ["Width"] = 0.25, // FIXME: why is it that 1x1 tile looks huge
                    ["Height"] = 0.25,
                    ["Collisions"] = false,
                    ["DestroyOnUse"] = true,
                    ["AutomaticInteraction"] = automaticInteraction,
                    ["TransferItemsOnActivate"] = true,
                }); ;
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
