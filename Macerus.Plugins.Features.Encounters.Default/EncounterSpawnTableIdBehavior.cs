using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.Encounters.Default
{
    public sealed class EncounterSpawnTableIdBehavior :
        BaseBehavior,
        IEncounterSpawnTableIdBehavior
    {
        public EncounterSpawnTableIdBehavior(
            IIdentifier spawnTableId,
            IEnumerable<IGeneratorComponent> additionalApawnGeneratorComponents)
        {
            SpawnTableId = spawnTableId;
            AdditionalSpawnGeneratorComponents = additionalApawnGeneratorComponents.ToArray();
        }

        public IIdentifier SpawnTableId { get; }

        public IReadOnlyCollection<IGeneratorComponent> AdditionalSpawnGeneratorComponents { get; }
    }
}
