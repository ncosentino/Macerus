using System.Threading.Tasks;

namespace Macerus.Plugins.Features.StatusBar.Api
{
    public interface IStatusBarController
    {
        Task UpdateAsync();
    }
}