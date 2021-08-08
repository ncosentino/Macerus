using System;
using System.Threading.Tasks;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Plugins.Features.Encounters
{
    public interface IEncounterManager
    {
        event EventHandler<EncounterChangedEventArgs> EncounterChanged;

        Task StartEncounterAsync(
            IFilterContext filterContext,
            IIdentifier encounterDefinitioId);

        Task EndEncounterAsync(IFilterContext filterContext);
    }
}