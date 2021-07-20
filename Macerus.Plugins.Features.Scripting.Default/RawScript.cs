
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Scripting.Default
{
    public sealed class RawScript : IRawScript
    {
        public RawScript(
            IIdentifier scriptId,
            string fullScriptContent,
            string fullyQualifiedClassName,
            bool singleInstance)
        {
            ScriptId = scriptId;
            FullScriptContent = fullScriptContent;
            FullyQualifiedClassName = fullyQualifiedClassName;
            SingleInstance = singleInstance;
        }

        public IIdentifier ScriptId { get; }

        public string FullScriptContent { get; }

        public string FullyQualifiedClassName { get; }

        public bool SingleInstance { get; }
    }
}
