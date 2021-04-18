using Macerus.Plugins.Features.CharacterSheet.Api;

namespace Macerus.Plugins.Features.CharacterSheet.Default
{
    public sealed class CharacterStatViewModel : ICharacterStatViewModel
    {
        public CharacterStatViewModel(
            string name,
            string displayValue)
        {
            Name = name;
            DisplayValue = displayValue;
        }

        public string Name { get; }

        public string DisplayValue { get; }
    }
}
