using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.Encounters.SpawnTables.Api;

using NexusLabs.Contracts;
using NexusLabs.Framework;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation.SpawnTables
{
    public sealed class ActorSpawner : IActorSpawner
    {
        private readonly ISpawnTableRepositoryFacade _spawnTableRepository;
        private readonly IAttributeFilterer _attributeFilterer;
        private readonly IRandom _random;
        private readonly ISpawnTableHandlerGeneratorFacade _spawnTableHandlerGeneratorFacade;

        public ActorSpawner(
            ISpawnTableRepositoryFacade spawnTableRepository,
            ISpawnTableHandlerGeneratorFacade spawnTableHandlerGeneratorFacade,
            IAttributeFilterer attributeFilterer,
            IRandom random)
        {
            _spawnTableRepository = spawnTableRepository;
            _spawnTableHandlerGeneratorFacade = spawnTableHandlerGeneratorFacade;
            _attributeFilterer = attributeFilterer;
            _random = random;
        }

        public IEnumerable<IGameObject> SpawnActors(IFilterContext filterContext)
        {
            // filter the spawn tables
            var allSpawnTables = _spawnTableRepository.GetAllSpawnTables();
            var filteredSpawnTables = _attributeFilterer
                .Filter(
                    allSpawnTables,
                    filterContext)
                .ToArray();
            if (filteredSpawnTables.Length < 1)
            {
                if (filterContext.MinimumCount < 1)
                {
                    yield break;
                }

                throw new InvalidOperationException(
                    $"There was no spawn table that could be selected from " +
                    $"the set of filtered spawn tables using context '{filterContext}'.");
            }

            Contract.Requires(
                filterContext.MinimumCount <= filterContext.MaximumCount,
                $"The generation context must have a maximum " +
                $"({filterContext.MaximumCount}) greater than or " +
                $"equal to the minimum ({filterContext.MinimumCount}).");
            Contract.Requires(
                filterContext.MinimumCount >= 0,
                $"The generation context must have a minimum " +
                $"({filterContext.MinimumCount}) greater than or " +
                $"equal to zero.");

            var targetCount = GetGenerationCount(
                filterContext.MinimumCount,
                filterContext.MaximumCount);
            var spawnTableCandidates = new HashSet<ISpawnTable>(filteredSpawnTables);
            for (var generationCount = 0; generationCount < targetCount; /* no increment here */)
            {
                // random roll the spawn table
                var spawnTable = spawnTableCandidates.RandomOrDefault(_random);
                if (spawnTable == null)
                {
                    if (generationCount >= filterContext.MinimumCount)
                    {
                        yield break;
                    }

                    throw new InvalidOperationException(
                        $"Randomized selection of spawn tables failed to select " +
                        $"a valid spawn table. Are any in the enumerable set?");
                }

                var generationCountBeforeSpawn = generationCount;
                var generatedLoot = _spawnTableHandlerGeneratorFacade.GenerateLoot(
                    spawnTable,
                    filterContext);
                foreach (var loot in generatedLoot)
                {
                    if (generationCount >= targetCount)
                    {
                        break;
                    }

                    yield return loot;
                    generationCount++;
                }

                // if this spawn table didn't yield any items, we can forget 
                // about it on future attempts with this context
                if (generationCount == generationCountBeforeSpawn)
                {
                    spawnTableCandidates.Remove(spawnTable);
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
    }
}