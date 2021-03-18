using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.Encounters
{
    public interface IEncounterRepository
    {
        IGameObject GetEncounterById(IIdentifier encounterDefinitionId);
    }
}
