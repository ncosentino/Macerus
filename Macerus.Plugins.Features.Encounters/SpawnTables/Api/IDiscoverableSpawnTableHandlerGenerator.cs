using System;

namespace Macerus.Plugins.Features.Encounters.SpawnTables.Api
{
    public interface IDiscoverableSpawnTableHandlerGenerator : ISpawnTableHandlerGenerator
    {
        Type SpawnTableType { get; }
    }
}