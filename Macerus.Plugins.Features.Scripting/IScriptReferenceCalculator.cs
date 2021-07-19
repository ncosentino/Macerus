using System.Collections.Generic;

namespace Macerus.Plugins.Features.Scripting
{
    public interface IScriptReferenceCalculator
    {
        IEnumerable<string> GetScriptReferencePaths(string rawScript);
    }
}
