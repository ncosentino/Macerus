using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Gui.MessageBoxes
{
    public class DialogBehaviour :
        MonoBehaviour,
        IDialogBehaviour
    {
        #region Unity Properties
        public Button YesButton;

        public Button NoButton;

        public Button CancelButton;

        public Button OkayButton;

        public Text Message;
        #endregion

        #region Properties
        Text IDialogBehaviour.Message { get { return Message; } }
        #endregion

        #region Methods
        public Button GetButton(ModalDialogButton modalDialogButton)
        {
            switch (modalDialogButton)
            {
                case ModalDialogButton.Yes:
                    return YesButton;
                case ModalDialogButton.No:
                    return NoButton;
                case ModalDialogButton.Okay:
                    return OkayButton;
                case ModalDialogButton.Cancel:
                    return CancelButton;
                default:
                    return null;
            }
        }
        #endregion
    }
}