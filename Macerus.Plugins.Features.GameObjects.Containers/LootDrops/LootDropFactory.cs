using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Plugins.Features.GameObjects.Containers.Api;
using Macerus.Plugins.Features.GameObjects.Containers.Api.LootDrops;

using NexusLabs.Contracts;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Macerus.Plugins.Features.GameObjects.Containers.LootDrops
{
    public sealed class LootDropFactory : ILootDropFactory
    {
        private readonly ILootDropIdentifiers _lootDropIdentifiers;
        private readonly IContainerRepository _containerRepository;
        private readonly IContainerIdentifiers _containerIdentifiers;
        private readonly IFilterContextFactory _filterContextFactory;
        private readonly IFilterContextAmenity _filterContextAmenity;

        public LootDropFactory(
            ILootDropIdentifiers lootDropIdentifiers,
            IContainerRepository containerRepository,
            IContainerIdentifiers containerIdentifiers,
            IFilterContextFactory filterContextFactory,
            IFilterContextAmenity filterContextAmenity)
        {
            _lootDropIdentifiers = lootDropIdentifiers;
            _containerRepository = containerRepository;
            _containerIdentifiers = containerIdentifiers;
            _filterContextFactory = filterContextFactory;
            _filterContextAmenity = filterContextAmenity;
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
            var filterContext = _filterContextFactory.CreateFilterContextForSingle();
            // note: if this seems redundant, it's because repos can handle 
            // multiple types if they choose... don't forget it! :)
            filterContext = _filterContextAmenity.ExtendWithGameObjectTypeIdFilter(
                filterContext,
                _containerIdentifiers.ContainerTypeIdentifier);
            filterContext = _filterContextAmenity.ExtendWithGameObjectTemplateIdFilter(
                filterContext,
                _lootDropIdentifiers.LootDropTemplateId);

            var lootObject = _containerRepository.CreateFromTemplate(
                filterContext,
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
