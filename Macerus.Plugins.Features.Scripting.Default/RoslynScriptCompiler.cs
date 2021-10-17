#if NET5_0_OR_GREATER
using System;
using System.Threading.Tasks;

namespace Macerus.Plugins.Features.Scripting.Default
{
    public sealed class RoslynScriptCompiler
        : IScriptCompiler
    {

        public async Task<ICompiledScript> CompileFromRawAsync(
            string rawScript,
            string fullClassName,
            bool singleInstance)
        {
            throw new NotSupportedException("// FIXME: support this for NET5.0+");
        }
    }
}
#endif