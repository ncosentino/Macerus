using System.Threading.Tasks;

using Macerus.Plugins.Features.Gui.Api;

namespace Macerus.Plugins.Features.StatusBar.Api
{
    public interface IStatusBarController : IDiscoverableUserInterfaceUpdate
    {
        Task ActivateSkillSlotAsync(int slotIndex);
    }
}