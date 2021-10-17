using System.Threading.Tasks;

namespace Macerus.Plugins.Features.Scripting.Default
{
    public sealed class SingleInstanceScript : ICompiledScript
    {
        private readonly IScript _wrappedScript;
        private readonly string _name;

        public SingleInstanceScript(
            IScript wrappedScript,
            string name)
        {
            _wrappedScript = wrappedScript;
            _name = name;
        }

        public Task RunAsync() => _wrappedScript.RunAsync();

        public override string ToString() =>
            $"Singleton Script: {_name}";
    }
}