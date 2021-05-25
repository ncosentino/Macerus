using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Encounters
{
    public interface IEncounterManager
    {
        void StartEncounter(IFilterContext filterContext, IIdentifier encounterDefinitioId);
    }
}