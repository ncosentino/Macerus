using System;

namespace Macerus.Plugins.Features.Encounters.SpawnTables
{
    public interface IDiscoverableSpawnTableHandlerGenerator : ISpawnTableHandlerGenerator
    {
        Type SpawnTableType { get; }
    }
}