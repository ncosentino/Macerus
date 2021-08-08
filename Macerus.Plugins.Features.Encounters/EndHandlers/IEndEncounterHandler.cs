using System.Threading.Tasks;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Plugins.Features.Encounters.EndHandlers
{
    public interface IEndEncounterHandler
    {
        Task HandleAsync(
            IGameObject encounter,
            IFilterContext filterContext);
    }
}
