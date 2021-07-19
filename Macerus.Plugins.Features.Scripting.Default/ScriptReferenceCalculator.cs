using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Macerus.Plugins.Features.Scripting.Default
{
    public sealed class ScriptReferenceCalculator : IScriptReferenceCalculator
    {
        private static readonly Regex USING_STATEMENT_REGEX = new Regex(
            @"using\s+(\S+);",
            RegexOptions.Compiled);

        public IEnumerable<string> GetScriptReferencePaths(string rawScript)
        {
            // FIXME: handle things like implicit dependencies that might not
            // show up in the using statements, but they are dependencies of
            // things that the using statements require. this isn't bad for a
            // first pass of this since people can just add extra usings
            // explicitly to cover this behavior.
            var usings = USING_STATEMENT_REGEX.Matches(rawScript);
            foreach (Match usingMatch in usings)
            {
                var path = $"{usingMatch.Groups[1].Value}.dll";
                if (!File.Exists(path))
                {
                    path = $"{usingMatch.Groups[1].Value}.exe";
                    if (!File.Exists(path))
                    {
                        continue;
                    }
                }

                yield return path;
            }
        }
    }
}
