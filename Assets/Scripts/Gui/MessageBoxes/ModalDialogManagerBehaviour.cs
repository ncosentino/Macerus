using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Linq;

namespace Assets.Scripts.Gui.MessageBoxes
{
    public sealed class ModalDialogManagerBehaviour : 
        MonoBehaviour,
        IModalDialogManagerBehaviour
    {
        #region Unity Properties
        public string PrefabResourcePath;

        public Canvas Canvas;
        #endregion

        #region Methods
        public void ShowMessage(string message)
        {
            ShowMessage(message, _ => { });
        }

        public void ShowMessage(string message, Action<ModalDialogButton> handler)
        {
            ShowMessage(message, new ModalDialogButtonHandler()
            {
                Button = ModalDialogButton.Okay,
                Handler = handler,
            });
        }

        public void ShowMessage(string message, params IModalDialogButtonHandler[] handlers)
        {
            var dialog = Instantiate(Resources.Load(PrefabResourcePath, typeof(GameObject))) as GameObject;
            
            var dialogBehaviour = dialog.GetComponentInChildren<IDialogBehaviour>();
            if (dialogBehaviour == null)
            {
                throw new InvalidOperationException("Can only show dialogs that implement 'IDialogBehaviour'.");
            }

            if (dialog.GetComponent<BringToFrontBehaviour>() == null)
            {
                dialog.AddComponent(typeof(BringToFrontBehaviour));
            }

            dialogBehaviour.Message.text = message;
            dialog.SetActive(true);

            foreach (var modalDialogButtonHandler in handlers)
            {
                var button = dialogBehaviour.GetButton(modalDialogButtonHandler.Button);
                if (button == null)
                {
                    throw new InvalidOperationException(string.Format(
                        "The provided prefab '{0}' does not have a button defined for '{1}'.",
                        PrefabResourcePath,
                        modalDialogButtonHandler.Button));
                }

                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => modalDialogButtonHandler.Handler.Invoke(modalDialogButtonHandler.Button));
                button.onClick.AddListener(() => CloseDialog(dialog));
            }

            foreach (ModalDialogButton modalDialogButton in Enum.GetValues(typeof(ModalDialogButton)))
            {
                var button = dialogBehaviour.GetButton(modalDialogButton);
                if (button == null)
                {
                    continue;
                }

                button.gameObject.SetActive(handlers.Select(x => x.Button).Contains(modalDialogButton));
            }

            dialog.transform.SetParent(Canvas.transform);
        }
        
        private void CloseDialog(GameObject dialog)
        {
            dialog.SetActive(false);
        }
        #endregion
    }

    public interface IModalDialogButtonHandler
    {
        ModalDialogButton Button { get; }

        Action<ModalDialogButton> Handler { get; }
    }

    public sealed class ModalDialogButtonHandler : IModalDialogButtonHandler
    {
        public ModalDialogButton Button { get; set; }

        public Action<ModalDialogButton> Handler { get; set; }
    }

    public enum ModalDialogButton
    {
        Yes,
        No,
        Cancel,
        Okay,        
    }

    public interface IDialogBehaviour
    {
        #region Properties
        Text Message { get; }
        #endregion

        #region Methods
        Button GetButton(ModalDialogButton modalDialogButton);
        #endregion
    }
}