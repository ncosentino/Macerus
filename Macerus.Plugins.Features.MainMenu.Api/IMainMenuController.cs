namespace Macerus.Plugins.Features.MainMenu.Api
{
    public interface IMainMenuController
    {
        void CloseMenu();

        void OpenMenu();

        bool ToggleMenu();
    }
}
