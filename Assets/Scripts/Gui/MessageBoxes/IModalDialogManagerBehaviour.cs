using System;

namespace Assets.Scripts.Gui.MessageBoxes
{
    public interface IModalDialogManagerBehaviour
    {
        void ShowMessage(string message);
        void ShowMessage(string message, Action<ModalDialogButton> handler);
        void ShowMessage(string message, params IModalDialogButtonHandler[] handlers);
    }
}