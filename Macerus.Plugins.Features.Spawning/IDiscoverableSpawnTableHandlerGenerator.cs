using System;

namespace Macerus.Plugins.Features.Spawning
{
    public interface IDiscoverableSpawnTableHandlerGenerator : ISpawnTableHandlerGenerator
    {
        Type SpawnTableType { get; }
    }
}