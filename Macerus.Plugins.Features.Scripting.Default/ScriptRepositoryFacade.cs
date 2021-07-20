using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Scripting.Default
{
    public sealed class ScriptRepositoryFacade : IScriptRepositoryFacade
    {
        private readonly Lazy<IReadOnlyCollection<IDiscoverableScriptRepository>> _scriptRepositories;

        public ScriptRepositoryFacade(Lazy<IEnumerable<IDiscoverableScriptRepository>> scriptRepositories)
        {
            _scriptRepositories = new Lazy<IReadOnlyCollection<IDiscoverableScriptRepository>>(() =>
                scriptRepositories.Value.ToArray());
        }

        public async Task<ICompiledScript> GetScriptByIdAsync(IIdentifier scriptId)
        {
            var tasks = _scriptRepositories
                .Value
                .Select(x => x.GetScriptByIdAsync(scriptId));
            var scripts = await Task.WhenAll(tasks);
            var script = scripts.FirstOrDefault(x => x != null);
            return script;
        }
    }
}
