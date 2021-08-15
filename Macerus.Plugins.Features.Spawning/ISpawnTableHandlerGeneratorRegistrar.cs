using System;

namespace Macerus.Plugins.Features.Spawning
{
    public interface ISpawnTableHandlerGeneratorRegistrar
    {
        void Register(
            Type SpawnTableType,
            ISpawnTableHandlerGenerator SpawnTableHandlerGenerator);
    }
}