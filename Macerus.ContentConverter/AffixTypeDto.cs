namespace Macerus.ContentConverter
{
    public sealed class AffixTypeDto
    {
        public AffixTypeDto(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; }

        public string Name { get; }
    }
}
