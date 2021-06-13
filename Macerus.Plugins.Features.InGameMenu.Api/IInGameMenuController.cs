namespace Macerus.Plugins.Features.InGameMenu.Api
{
    public interface IInGameMenuController
    {
        void CloseMenu();

        void OpenMenu();

        bool ToggleMenu();
    }
}
