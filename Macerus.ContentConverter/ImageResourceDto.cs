namespace Macerus.ContentConverter
{
    public sealed class ImageResourceDto
    {
        public ImageResourceDto(string relativeResourcePath)
        {
            RelativeResourcePath = relativeResourcePath;
        }

        public string RelativeResourcePath { get; }
    }
}
