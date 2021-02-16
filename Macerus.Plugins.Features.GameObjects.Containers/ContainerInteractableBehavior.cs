using System;
using System.Linq;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.GameObjects.Containers.Api;

using NexusLabs.Contracts;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.Behaviors;
using ProjectXyz.Shared.Game.GameObjects.Generation;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;

namespace Macerus.Plugins.Features.GameObjects.Containers
{
    public sealed class ContainerInteractableBehavior : 
        BaseBehavior,
        IInteractableBehavior
    {
        private readonly IMutableGameObjectManager _gameObjectManager;
        private readonly ILootGenerator _lootGenerator;
        private readonly IGeneratorContextProvider _generatorContextProvider;
        private readonly IDropTableRepositoryFacade _dropTableRepository;

        public ContainerInteractableBehavior(
            IMutableGameObjectManager gameObjectManager,
            ILootGenerator lootGenerator,
            IGeneratorContextProvider generatorContextProvider,
            IDropTableRepositoryFacade dropTableRepository,
            bool automaticInteraction)
        {
            _gameObjectManager = gameObjectManager;
            _lootGenerator = lootGenerator;
            _generatorContextProvider = generatorContextProvider;
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
                _gameObjectManager.MarkForRemoval((IGameObject)Owner); // FIXME: whyyyyy this terrible casting
            }
        }

        private void GenerateNecessaryLoot(IItemContainerBehavior sourceItemContainer)
        {
            IGeneratorContext baseGeneratorContext = null;
            foreach (var containerGenerateItemsBehavior in Owner.Get<IContainerGenerateItemsBehavior>())
            {
                if (containerGenerateItemsBehavior.HasGeneratedItems)
                {
                    continue;
                }

                if (baseGeneratorContext == null)
                {
                    baseGeneratorContext = _generatorContextProvider.GetGeneratorContext();
                }

                var dropTableId = containerGenerateItemsBehavior.DropTableId;
                var dropTable = _dropTableRepository.GetForDropTableId(dropTableId);
                var dropTableAttribute = new GeneratorAttribute(
                    new StringIdentifier("drop-table"),
                    new IdentifierGeneratorAttributeValue(dropTableId),
                    true);

                var generatorContext = baseGeneratorContext
                    .WithAdditionalAttributes(new[] { dropTableAttribute })
                    .WithGenerateCountRange(
                        dropTable.MinimumGenerateCount,
                        dropTable.MaximumGenerateCount);

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

                containerGenerateItemsBehavior.HasGeneratedItems = true;
            }
        }
    }
}
