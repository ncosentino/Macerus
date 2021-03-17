using System;

namespace Macerus.Plugins.Features.Encounters.SpawnTables.Api
{
    public interface ISpawnTableHandlerGeneratorRegistrar
    {
        void Register(
            Type SpawnTableType,
            ISpawnTableHandlerGenerator SpawnTableHandlerGenerator);
    }
}