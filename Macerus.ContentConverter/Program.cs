namespace Macerus.ContentConverter
{
    internal sealed class Program
    {
        static void Main(string[] args)
        {
            var gameDataUrl = args[0];
            var sheetHelper = new SheetHelper();
            var contentConverter = new ExcelContentConverter(sheetHelper);
            contentConverter.Convert(gameDataUrl);
        }
    }
}
