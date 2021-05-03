using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Features.Encounters
{
    public interface IEncounterSpawnTableIdBehavior : IBehavior
    {
        IIdentifier SpawnTableId { get; }

        IReadOnlyCollection<IGeneratorComponent> AdditionalSpawnGeneratorComponents { get; }
    }
}
