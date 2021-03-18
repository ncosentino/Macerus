using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Encounters
{
    public interface IEncounterManager
    {
        void StartEncounter(IFilterContext filterContext, IIdentifier encounterDefinitioId);
    }
}