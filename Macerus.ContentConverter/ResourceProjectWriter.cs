using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Macerus.ContentConverter
{
    public sealed class ResourceProjectWriter
    {
        public void WriteProjectContents(
            string macerusContentProjectDirectory,
            IEnumerable<string> resourceContentFilePaths)
        {
            var projectFilePath = Path.Combine(macerusContentProjectDirectory, "Macerus.Content.csproj");

            // FIXME: don't read everything in...
            var projectContents = File.ReadAllText(projectFilePath);
            bool performedReplacement = false;
            projectContents = Regex.Replace(
                projectContents,
                @"<ItemGroup>(\s+<Content.*>.*\s*.*\s*.*\s*</Content>\s+)+</ItemGroup>",
                x =>
                {
                    performedReplacement = true;
                    return $"<ItemGroup>\r\n{string.Join("\r\n", GetResourceBlockCode(macerusContentProjectDirectory.Length, resourceContentFilePaths))}\r\n</ItemGroup>";
                });

            if (!performedReplacement)
            {
                throw new InvalidOperationException("Could not find the content section of the csproj to replace. Consider bolstering this to just add it in.");
            }

            File.WriteAllText(projectFilePath, projectContents);
        }

        private IEnumerable<string> GetResourceBlockCode(
            int lengthOfMacerusContentProjectDirectory,
            IEnumerable<string> resourceFilePaths)
        {
            foreach (var resourceFilePath in resourceFilePaths)
            {
                var codeBlock = @$"    <Content Include=""{resourceFilePath.Substring(lengthOfMacerusContentProjectDirectory).TrimStart('\\')}"">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
      <PackageCopyToOutput>true</PackageCopyToOutput>
    </Content>";
                yield return codeBlock;
            }
        }
    }
}
