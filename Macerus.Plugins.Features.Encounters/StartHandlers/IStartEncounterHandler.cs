using System.Threading.Tasks;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Plugins.Features.Encounters
{
    public interface IStartEncounterHandler
    {
        Task HandleAsync(
            IGameObject encounter,
            IFilterContext filterContext);
    }
}
