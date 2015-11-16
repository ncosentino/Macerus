using System;

namespace Assets.Scripts.Gui.MessageBoxes
{
    public interface IModalDialogManagerBehaviour
    {
        #region Methods
        void ShowMessage(Guid stringResourceId);

        void ShowMessage(
            Guid stringResourceId,
            object[] stringParameters);

        void ShowMessage(
            Guid stringResourceId,
            Action<ModalDialogButton> handler);

        void ShowMessage(
            Guid stringResourceId,
            object[] stringParameters, 
            Action<ModalDialogButton> handler);

        void ShowMessage(
            Guid stringResourceId,
            params IModalDialogButtonHandler[] handlers);

        void ShowMessage(
            Guid stringResourceId,
            object[] stringParameters, 
            params IModalDialogButtonHandler[] handlers);
        #endregion
    }
}