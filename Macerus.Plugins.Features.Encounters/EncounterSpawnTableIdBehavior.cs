using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.Encounters
{
    public sealed class EncounterSpawnTableIdBehavior :
        BaseBehavior,
        IEncounterSpawnTableIdBehavior
    {
        public EncounterSpawnTableIdBehavior(IIdentifier spawnTableId)
        {
            SpawnTableId = spawnTableId;
        }

        public IIdentifier SpawnTableId { get; }        
    }
}
