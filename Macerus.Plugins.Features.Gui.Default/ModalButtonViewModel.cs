using System;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Gui.Default
{
    public sealed class ModalButtonViewModel : IModalButtonViewModel
    {
        private readonly Action<IModalButtonViewModel> _buttonSelectedCallback;

        public ModalButtonViewModel(
            IIdentifier stringResourceId,
            Action<IModalButtonViewModel> buttonSelectedCallback)
        {
            StringResourceId = stringResourceId;
            _buttonSelectedCallback = buttonSelectedCallback;
        }

        public IIdentifier StringResourceId { get; }

        public void ButtonSelected() => _buttonSelectedCallback?.Invoke(this);
    }
}
