using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Macerus.ContentConverter
{
    public sealed class ResourceConverter
    {
        public ConvertedResourceContent Convert(string macerusContentProjectDirectory)
        {
            var resourceFilePaths = GetResourceFilePaths(macerusContentProjectDirectory).ToArray();
            var convertedResourceContent = new ConvertedResourceContent(resourceFilePaths);
            return convertedResourceContent;
        }

        private IEnumerable<string> GetResourceFilePaths(string macerusContentProjectDirectory)
        {
            var resourceFolderNames = new string[]
            {
                @"Mapping\Maps",
                "Resources",
            };

            foreach (var resourceFolderName in resourceFolderNames)
            {
                var resourcesDirectory = Path.Combine(macerusContentProjectDirectory, resourceFolderName);

                foreach (var resourceFile in Directory.GetFiles(
                    resourcesDirectory,
                    "*.*",
                    new EnumerationOptions() { RecurseSubdirectories = true })
                    .Where(x => !x.EndsWith(".cs")))
                {
                    yield return resourceFile;
                }
            }
        }
    }
}
