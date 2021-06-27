using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.GameObjects.Containers.Api.LootDrops;
using Macerus.Plugins.Features.Inventory.Api;

using NexusLabs.Contracts;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Mapping;

namespace Macerus.Plugins.Features.Inventory.Default
{
    public sealed class DropToMapItemSet : IItemSet
    {
        private readonly ILootDropFactory _lootDropFactory;
        private readonly ILootDropIdentifiers _lootDropIdentifiers;
        private readonly IMapGameObjectManager _mapGameObjectManager;
        private readonly IMapProvider _mapProvider;

        public DropToMapItemSet(
            ILootDropFactory lootDropFactory,
            ILootDropIdentifiers lootDropIdentifiers,
            IMapGameObjectManager mapGameObjectManager,
            IMapProvider mapProvider)
        {
            _lootDropFactory = lootDropFactory;
            _lootDropIdentifiers = lootDropIdentifiers;
            _mapGameObjectManager = mapGameObjectManager;
            _mapProvider = mapProvider;
        }

        public event EventHandler<EventArgs> ItemsChanged;

        public IEnumerable<KeyValuePair<IIdentifier, IGameObject>> Items => Enumerable.Empty<KeyValuePair<IIdentifier, IGameObject>>();

        public IGameObject GetItem(IIdentifier id) => null;

        public bool CanSwapItems(
            IIdentifier itemIdToSwapOut,
            IGameObject itemToSwapIn)
        {
            Contract.Requires(
                itemIdToSwapOut == null,
                $"Expecting that '{nameof(itemIdToSwapOut)}' is always null.");
            Contract.RequiresNotNull(
                itemToSwapIn,
                $"Expecting that '{nameof(itemToSwapIn)}' is never null.");

            return true;
        }

        public SwapResult SwapItems(
            IIdentifier itemIdToSwapOut,
            IGameObject itemToSwapIn)
        {
            var activePlayerCharacter = _mapGameObjectManager
                .GameObjects
                .First(x => x.Has<IPlayerControlledBehavior>());
            var playerPositionBehavior = activePlayerCharacter.GetOnly<IReadOnlyPositionBehavior>();
            var playerSizeBehavior = activePlayerCharacter.GetOnly<IReadOnlySizeBehavior>();

            var loot = _mapProvider
                .PathFinder
                .GetIntersectingGameObjects(
                    new Vector2((float)playerPositionBehavior.X, (float)playerPositionBehavior.Y),
                    new Vector2((float)playerSizeBehavior.Width, (float)playerSizeBehavior.Height))
                .FirstOrDefault(x =>
                {
                    if (!x.TryGetFirst<ICreatedFromTemplateBehavior>(out var createdFromTemplateBehavior))
                    {
                        return false;
                    }

                    if (!Equals(createdFromTemplateBehavior.TemplateId, _lootDropIdentifiers.LootDropTemplateId))
                    {
                        return false;
                    }

                    var lootPositionBehavior = x.GetOnly<IReadOnlyPositionBehavior>();
                    var lootSizeBehavior = x.GetOnly<IReadOnlySizeBehavior>();

                    var itemContainerBehavior = x.GetOnly<IItemContainerBehavior>();
                    var addedItem = itemContainerBehavior.TryAddItem(itemToSwapIn);
                    return addedItem;
                });
            if (loot == null)
            {
                loot = _lootDropFactory.CreateLoot(
                    playerPositionBehavior.X,
                    playerPositionBehavior.Y,
                    false,
                    new[]
                    {
                        itemToSwapIn
                    });
                _mapGameObjectManager.MarkForAddition(loot);
            }

            return SwapResult.SuccessAndContinue;
        }
    }
}
