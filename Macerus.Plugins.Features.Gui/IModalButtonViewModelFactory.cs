using System;

namespace Macerus.Plugins.Features.Gui
{
    public interface IModalButtonViewModelFactory
    {
        IModalButtonViewModel CreateOkButton(Action<IModalButtonViewModel> buttonSelectedCallback);

        IModalButtonViewModel CreateYesButton(Action<IModalButtonViewModel> buttonSelectedCallback);

        IModalButtonViewModel CreateNoButton(Action<IModalButtonViewModel> buttonSelectedCallback);

        IModalButtonViewModel CreateCancelButton(Action<IModalButtonViewModel> buttonSelectedCallback);
    }
}
