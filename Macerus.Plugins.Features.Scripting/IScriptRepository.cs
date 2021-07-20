using System.Threading.Tasks;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Scripting
{
    public interface IScriptRepository
    {
        Task<ICompiledScript> GetScriptByIdAsync(IIdentifier scriptId);
    }
}
