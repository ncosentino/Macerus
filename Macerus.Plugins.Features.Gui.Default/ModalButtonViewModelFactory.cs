using System;

using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.Gui.Default
{
    public sealed class ModalButtonViewModelFactory : IModalButtonViewModelFactory
    {
        public IModalButtonViewModel CreateCancelButton(Action<IModalButtonViewModel> buttonSelectedCallback)
        {
            // FIXME: use a resource ID for lookup
            var button = new ModalButtonViewModel(
                new StringIdentifier("Cancel"),
                b => buttonSelectedCallback?.Invoke(b));
            return button;
        }

        public IModalButtonViewModel CreateNoButton(Action<IModalButtonViewModel> buttonSelectedCallback)
        {
            // FIXME: use a resource ID for lookup
            var button = new ModalButtonViewModel(
                new StringIdentifier("No"),
                b => buttonSelectedCallback?.Invoke(b));
            return button;
        }

        public IModalButtonViewModel CreateOkButton(Action<IModalButtonViewModel> buttonSelectedCallback)
        {
            // FIXME: use a resource ID for lookup
            var button = new ModalButtonViewModel(
                new StringIdentifier("OK"),
                b => buttonSelectedCallback?.Invoke(b));
            return button;
        }

        public IModalButtonViewModel CreateYesButton(Action<IModalButtonViewModel> buttonSelectedCallback)
        {
            // FIXME: use a resource ID for lookup
            var button = new ModalButtonViewModel(
                new StringIdentifier("Yes"),
                b => buttonSelectedCallback?.Invoke(b));
            return button;
        }
    }
}
