﻿using System;
using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using Macerus.Plugins.Features.Spawning;

namespace Macerus.Plugins.Features.Spawning.Default
{
    public sealed class SpawnTableHandlerGeneratorFacade : ISpawnTableHandlerGeneratorFacade
    {
        private readonly Dictionary<Type, ISpawnTableHandlerGenerator> _mapping;

        public SpawnTableHandlerGeneratorFacade()
        {
            _mapping = new Dictionary<Type, ISpawnTableHandlerGenerator>();
        }

        public IEnumerable<IGameObject> GenerateActors(
            ISpawnTable spawnTable,
            IFilterContext filterContext,
            IEnumerable<IGeneratorComponent> additionalActorGeneratorComponents)
        {
            if (!_mapping.TryGetValue(
                spawnTable.GetType(),
                out var spawnTableHandlerGenerator))
            {
                throw new InvalidOperationException(
                    $"No supported spawn table handler generator for type '{spawnTable.GetType()}'.");
            }

            var generated = spawnTableHandlerGenerator.GenerateActors(
                spawnTable,
                filterContext,
                additionalActorGeneratorComponents);
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