using System;

namespace Macerus.Plugins.Features.Encounters.SpawnTables
{
    public interface ISpawnTableHandlerGeneratorRegistrar
    {
        void Register(
            Type SpawnTableType,
            ISpawnTableHandlerGenerator SpawnTableHandlerGenerator);
    }
}