namespace Macerus.Plugins.Features.MainMenu.Api.NewGame
{
    public interface INewGameController
    {
        void CloseScreen();

        void ShowScreen();

        bool ToggleScreen();
    }
}