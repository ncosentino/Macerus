using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Encounters
{
    public interface IEncounterSpawnTableIdBehavior : IBehavior
    {
        IIdentifier SpawnTableId { get; }
    }
}
