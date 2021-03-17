using System;
using System.Collections.Generic;

using Macerus.Plugins.Features.Encounters.SpawnTables.Api;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation.SpawnTables
{
    public sealed class SpawnTableHandlerGeneratorFacade : ISpawnTableHandlerGeneratorFacade
    {
        private readonly Dictionary<Type, ISpawnTableHandlerGenerator> _mapping;

        public SpawnTableHandlerGeneratorFacade(IEnumerable<IDiscoverableSpawnTableHandlerGenerator> generators)
        {
            _mapping = new Dictionary<Type, ISpawnTableHandlerGenerator>();
            foreach (var generator in generators)
            {
                Register(generator.SpawnTableType, generator);
            }
        }

        public IEnumerable<IGameObject> GenerateLoot(
            ISpawnTable spawnTable,
            IFilterContext filterContext)
        {
            if (!_mapping.TryGetValue(
                spawnTable.GetType(),
                out var spawnTableHandlerGenerator))
            {
                throw new InvalidOperationException(
                    $"No supported spawn table handler generator for type '{spawnTable.GetType()}'.");
            }

            var generated = spawnTableHandlerGenerator.GenerateLoot(
                spawnTable,
                filterContext);
            return generated;
        }

        public void Register(
            Type spawnTableType,
            ISpawnTableHandlerGenerator spawnTableHandlerGenerator)
        {
            _mapping.Add(spawnTableType, spawnTableHandlerGenerator);
        }
    }
}