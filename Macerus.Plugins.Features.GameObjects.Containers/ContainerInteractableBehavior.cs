using System;
using System.Globalization;
using System.Linq;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.GameObjects.Containers.Api;

using NexusLabs.Contracts;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Containers
{
    public sealed class ContainerInteractableBehavior : 
        BaseBehavior,
        IInteractableBehavior
    {
        private readonly IMutableGameObjectManager _gameObjectManager;
        private readonly ILootGenerator _lootGenerator;
        private readonly IGeneratorContextProvider _generatorContextProvider;

        public ContainerInteractableBehavior(
            IMutableGameObjectManager gameObjectManager,
            ILootGenerator lootGenerator,
            IGeneratorContextProvider generatorContextProvider,
            bool automaticInteraction)
        {
            _gameObjectManager = gameObjectManager;
            _lootGenerator = lootGenerator;
            _generatorContextProvider = generatorContextProvider;
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

            IGeneratorContext generatorContext = null;
            foreach (var containerGenerateItemsBehavior in Owner.Get<IReadOnlyContainerGenerateItemsBehavior>())
            {
                if (generatorContext == null)
                {
                    // FIXME: how do we take properties from this container to 
                    // influence the loot that we're dropping? How do we modify
                    // this context to support this?
                    generatorContext = _generatorContextProvider.GetGeneratorContext();
                }

                var generatedItems = _lootGenerator.GenerateLoot(generatorContext);
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

                // FIXME: we need a way to prevent item generation using this 
                // behavior the next time this is interacted with
            }

            if (AutomaticInteraction)
            {
                // need a copy so we can iterate + remove
                var itemsToTake = sourceItemContainer
                    .Items
                    .ToArray();
                foreach (var item in itemsToTake)
                {
                    sourceItemContainer.TryRemoveItem(item);
                    actorInventory.TryAddItem(item);
                }
            }

            if (Owner.GetOnly<IReadOnlyContainerPropertiesBehavior>().DestroyOnUse)
            {
                _gameObjectManager.MarkForRemoval((IGameObject)Owner); // FIXME: whyyyyy this terrible casting
            }
        }
    }
}
