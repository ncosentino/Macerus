
using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.Encounters
{
    public interface IStartEncounterHandler
    {
        void Handle(
            IGameObject encounter,
            IFilterContext filterContext);
    }
}
