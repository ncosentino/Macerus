
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Scripting
{
    public interface IRawScript
    {
        string FullScriptContent { get; }

        string FullyQualifiedClassName { get; }

        IIdentifier ScriptId { get; }

        bool SingleInstance { get; }
    }
}
