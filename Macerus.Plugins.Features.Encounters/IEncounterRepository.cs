using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Plugins.Features.Encounters
{
    public interface IEncounterRepository
    {
        IGameObject GetEncounterById(
            IFilterContext filterContext,
            IIdentifier encounterDefinitionId);
    }
}
