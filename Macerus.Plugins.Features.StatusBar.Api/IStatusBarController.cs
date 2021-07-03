using System.Threading.Tasks;

using Macerus.Plugins.Features.Gui;

using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.StatusBar.Api
{
    public interface IStatusBarController : IDiscoverableUserInterfaceUpdate
    {
        Task ActivateSkillSlotAsync(
            IGameObject actor,
            int slotIndex);

        Task PreviewSkillSlotAsync(
            IGameObject actor,
            int slotIndex);
    }
}