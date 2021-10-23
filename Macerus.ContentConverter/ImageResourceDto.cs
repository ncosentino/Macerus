namespace Macerus.ContentConverter
{
    public sealed class ImageResourceDto
    {
        public ImageResourceDto(
            string imageResourceId,
            string value)
        {
            ImageResourceId = imageResourceId;
            Value = value;
        }

        public string ImageResourceId { get; }

        public string Value { get; }
    }
}
