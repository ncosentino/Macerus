using Macerus.Plugins.Features.Gui;

namespace Macerus.Plugins.Features.PartyBar
{
    public interface IPartyBarController : IDiscoverableUserInterfaceUpdate
    {
        void ShowPartyBar();

        void ClosePartyBar();
    }
}
