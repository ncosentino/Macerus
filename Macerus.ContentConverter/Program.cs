using System.IO;

namespace Macerus.ContentConverter
{
    internal sealed class Program
    {
        static void Main(string[] args)
        {
            var gameDataUrl = args[0];
            var outputDirectory = args[1];
            outputDirectory = new DirectoryInfo(outputDirectory).FullName;

            var sheetHelper = new SheetHelper();
            var contentConverter = new ExcelContentConverter(sheetHelper);
            contentConverter.Convert(
                gameDataUrl,
                outputDirectory);

            var imageResourceConverter = new ResourceConverter();
            var resourceProjectWriter = new ResourceProjectWriter();
            var convertedResourceContent = imageResourceConverter.Convert(outputDirectory);
            resourceProjectWriter.WriteProjectContents(
                outputDirectory,
                convertedResourceContent.ResourceContentFilePaths);
        }
    }
}
