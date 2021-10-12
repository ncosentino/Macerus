using System.Threading.Tasks;

using Macerus.Plugins.Features.Gui;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Plugins.Features.StatusBar.Api
{
    public interface IStatusBarController : IDiscoverableUserInterfaceUpdate
    {
        Task ActivateSkillSlotAsync(
            IFilterContext filterContext,
            IGameObject actor,
            int slotIndex);

        Task ClearSkillSlotPreviewAsync();

        Task PreviewSkillSlotAsync(
            IFilterContext filterContext,
            IGameObject actor,
            int slotIndex);
    }
}