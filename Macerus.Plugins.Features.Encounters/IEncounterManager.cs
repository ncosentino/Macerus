using System.Threading.Tasks;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Plugins.Features.Encounters
{
    public interface IEncounterManager
    {
        Task StartEncounterAsync(
            IFilterContext filterContext,
            IIdentifier encounterDefinitioId);
    }
}