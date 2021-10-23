namespace Macerus.ContentConverter
{
    public sealed class StringResourceDto
    {
        public StringResourceDto(
            string stringResourceId, 
            string value)
        {
            StringResourceId = stringResourceId;
            Value = value;
        }

        public string StringResourceId { get; }

        public string Value { get; }
    }
}
