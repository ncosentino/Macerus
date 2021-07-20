using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Scripting.Default
{
    public sealed class InMemoryScriptRepository : IDiscoverableScriptRepository
    {
        private readonly IScriptCompiler _scriptCompiler;
        private readonly Dictionary<IIdentifier, ICompiledScript> _compiledScripts;
        private readonly Dictionary<IIdentifier, IRawScript> _rawScripts;

        public InMemoryScriptRepository(
            IScriptCompiler scriptCompiler,
            IEnumerable<IRawScript> rawScripts)
        {
            _compiledScripts = new Dictionary<IIdentifier, ICompiledScript>();
            _scriptCompiler = scriptCompiler;
            _rawScripts = rawScripts.ToDictionary(
                x => x.ScriptId,
                x => x);
        }

        public async Task<ICompiledScript> GetScriptByIdAsync(IIdentifier scriptId)
        {
            if (_compiledScripts.TryGetValue(
                scriptId,
                out var compiledScript))
            {
                return compiledScript;
            }

            if (!_rawScripts.TryGetValue(
                scriptId,
                out var rawScript))
            {
                return null;
            }

            compiledScript = await _scriptCompiler
                .CompileFromRawAsync(
                    rawScript.FullScriptContent,
                    rawScript.FullyQualifiedClassName,
                    rawScript.SingleInstance)
                .ConfigureAwait(false);
            _compiledScripts[scriptId] = compiledScript;
            return compiledScript;
        }
    }
}
