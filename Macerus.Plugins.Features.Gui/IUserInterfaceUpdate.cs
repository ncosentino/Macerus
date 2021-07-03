using System.Threading.Tasks;

using ProjectXyz.Api.Systems;

namespace Macerus.Plugins.Features.Gui
{
    public interface IUserInterfaceUpdate
    {
        Task UpdateAsync(ISystemUpdateContext systemUpdateContext);
    }
}
