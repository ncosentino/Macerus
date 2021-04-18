namespace Macerus.Plugins.Features.CharacterSheet.Api
{
    public interface ICharacterSheetController
    {
        void OpenCharacterSheet();

        void CloseCharacterSheet();

        bool ToggleCharacterSheet();
    }
}