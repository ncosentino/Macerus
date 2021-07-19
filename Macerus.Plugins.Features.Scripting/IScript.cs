using System.Threading.Tasks;

namespace Macerus.Plugins.Features.Scripting
{
    public interface IScript
    {
        Task RunAsync();
    }
}
