using System.Threading.Tasks;

using ProjectXyz.Api.Systems;

namespace Macerus.Plugins.Features.Gui.Api
{
    public interface IUserInterfaceUpdate
    {
        Task UpdateAsync(ISystemUpdateContext systemUpdateContext);
    }
}
