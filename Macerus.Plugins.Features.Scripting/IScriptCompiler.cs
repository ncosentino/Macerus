using System;
using System.Threading.Tasks;

namespace Macerus.Plugins.Features.Scripting
{
    public interface IScriptCompiler
    {
        Task<ICompiledScript> CompileFromRawAsync(
            string rawScript,
            string fullClassName,
            bool singleInstance);
    }
}
