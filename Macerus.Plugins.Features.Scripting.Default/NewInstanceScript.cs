using System;
using System.Threading.Tasks;

namespace Macerus.Plugins.Features.Scripting.Default
{
    public sealed class NewInstanceScript : ICompiledScript
    {
        private readonly Func<IScript> _createScriptCallback;
        private readonly string _name;

        public NewInstanceScript(
            Func<IScript> createScriptCallback,
            string name)
        {
            _createScriptCallback = createScriptCallback;
            _name = name;
        }

        public async Task RunAsync()
        {
            var instance = _createScriptCallback.Invoke();
            await instance.RunAsync().ConfigureAwait(false);
        }

        public override string ToString() =>
            $"New-Instance Script: {_name}";
    }
}