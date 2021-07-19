using System;
using System.Threading.Tasks;

namespace Macerus.Plugins.Features.Scripting
{
    public interface IScriptCompiler
    {
        Task<IScript> CompileFromRawAsync(
            string rawScript,
            string fullClassName);
    }
}
