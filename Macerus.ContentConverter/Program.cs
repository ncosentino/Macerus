namespace Macerus.ContentConverter
{
    internal sealed class Program
    {
        static void Main(string[] args)
        {
            var gameDataUrl = args[0];
            var contentConverter = new ExcelContentConverter();
            contentConverter.Convert(gameDataUrl);
        }
    }
}
