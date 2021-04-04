using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.Encounters.SpawnTables.Api;
using Macerus.Plugins.Features.Encounters.SpawnTables.Api.Linked;

using NexusLabs.Contracts;
using NexusLabs.Framework;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes; // FIXME: dependency on non-API

namespace Macerus.Plugins.Features.Encounters.SpawnTables.Implementations.Linked
{
    public sealed class LinkedSpawnTableHandlerGenerator : IDiscoverableSpawnTableHandlerGenerator
    {
        private readonly IRandom _random;
        private readonly ISpawnTableHandlerGeneratorFacade _spawnTableHandlerGeneratorFacade;
        private readonly ISpawnTableRepositoryFacade _spawnTableRepository;
        private readonly IFilterContextFactory _filterContextFactory;

        public LinkedSpawnTableHandlerGenerator(
            IRandom random,
            ISpawnTableHandlerGeneratorFacade spawnTableHandlerGeneratorFacade,
            ISpawnTableRepositoryFacade spawnTableRepository,
            IFilterContextFactory filterContextFactory)
        {
            _random = random;
            _spawnTableHandlerGeneratorFacade = spawnTableHandlerGeneratorFacade;
            _spawnTableRepository = spawnTableRepository;
            _filterContextFactory = filterContextFactory;
        }

        public Type SpawnTableType { get; } = typeof(LinkedSpawnTable);

        public IEnumerable<IGameObject> GenerateActors(
            ISpawnTable spawnTable,
            IFilterContext filterContext,
            IEnumerable<IGeneratorComponent> additionalActorGeneratorComponents)
        {
            Contract.Requires(
                spawnTable.GetType() == SpawnTableType,
                $"The provided spawn table '{spawnTable}' must have the type '{SpawnTableType}'.");
            return GenerateActors(
                (ILinkedSpawnTable)spawnTable,
                filterContext,
                additionalActorGeneratorComponents);
        }

        private IEnumerable<IGameObject> GenerateActors(
            ILinkedSpawnTable spawnTable,
            IFilterContext filterContext,
            IEnumerable<IGeneratorComponent> additionalActorGeneratorComponents)
        {
            // get a random count for this spawn table
            var generationCount = GetGenerationCount(
                spawnTable.MinimumGenerateCount,
                spawnTable.MaximumGenerateCount);

            // create our new context by keeping information about attributes 
            // from our caller, but acknowledging that any that were required
            // are now fulfilled up until this point. we then cobine in the
            // newly provided attributes from the spawn table.
            var currentSpawnContext = _filterContextFactory.CreateContext(
                spawnTable.MinimumGenerateCount,
                spawnTable.MaximumGenerateCount,
                filterContext
                    .Attributes
                    .Select(x => x.Required
                        ? x.CopyWithRequired(false)
                        : x)
                    .Concat(spawnTable.ProvidedAttributes));

            // calculate the total weight once
            var totalWeight = spawnTable.Entries.Sum(x => x.Weight);

            for (var i = 0; i < generationCount; i++)
            {
                // pick an entry
                var entry = PickWeightedEntry(
                    spawnTable.Entries,
                    totalWeight);
                var linkedSpawnTableId = entry.SpawnTableId;

                // load the new spawn table
                var linkedSpawnTable = _spawnTableRepository.GetForSpawnTableId(linkedSpawnTableId);

                // Create a new context
                var linkedSpawnContext = _filterContextFactory.CreateContext(
                    linkedSpawnTable.MinimumGenerateCount,
                    linkedSpawnTable.MaximumGenerateCount,
                    currentSpawnContext.Attributes.Concat(linkedSpawnTable.ProvidedAttributes));

                // delegate generation of this table to someone else
                var generatorComponents = linkedSpawnTable
                    .ProvidedGeneratorComponents
                    .Concat(additionalActorGeneratorComponents);
                var generated = _spawnTableHandlerGeneratorFacade.GenerateActors(
                    linkedSpawnTable,
                    linkedSpawnContext,
                    generatorComponents);
                foreach (var gameObject in generated)
                {
                    yield return gameObject;
                }
            }
        }

        private int GetGenerationCount(
            int itemCountMinimum,
            int itemCountMaximum)
        {
            var count = _random.Next(
                itemCountMinimum,
                itemCountMaximum == int.MaxValue
                    ? int.MaxValue
                    : itemCountMaximum + 1);
            return count;
        }

        private IWeightedEntry PickWeightedEntry(
            IEnumerable<IWeightedEntry> entries,
            double totalWeight)
        {
            var randomNumber = _random.NextDouble(0, totalWeight);

            foreach (var entry in entries)
            {
                if (randomNumber <= entry.Weight)
                {
                    return entry;
                }

                randomNumber = randomNumber - entry.Weight;
            }

            throw new InvalidOperationException("No weighted entry was selected.");
        }
    }
}