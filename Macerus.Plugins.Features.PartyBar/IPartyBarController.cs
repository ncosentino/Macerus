using Macerus.Plugins.Features.Gui;

namespace Macerus.Plugins.Features.PartyBar
{
    public interface IPartyBarController : IUserInterfaceUpdate
    {
        void ShowPartyBar();

        void ClosePartyBar();
    }
}
