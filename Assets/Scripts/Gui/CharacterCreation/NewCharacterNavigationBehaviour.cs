using System;
using System.Linq;
using Assets.Scripts.Gui.MessageBoxes;
using ProjectXyz.Api.Messaging.Core.CharacterCreation;
using ProjectXyz.Api.Messaging.Core.General;
using ProjectXyz.Api.Messaging.Core.Initialization;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Gui
{
    public sealed class NewCharacterNavigationBehaviour : MonoBehaviour
    {
        #region Fields
        private INewCharacterController _newCharacterController;
        #endregion

        #region Unity Properties
        public RpcBehaviour Rpc;

        public ModalDialogManagerBehaviour ModalDialogManager;

        public InputField CharacterNameInputField;
        #endregion

        #region Methods
        private void Start()
        {
            GetNewCharacterOptionsResponse response;
            if (!Rpc.Client.TrySend(
                new GetNewCharacterOptionsRequest(),
                TimeSpan.FromSeconds(5),
                out response))
            {
                ModalDialogManager.ShowMessage("Could not get the character creation options.");
                return;
            }

            // TODO: populate the set of optional races

            // TODO: populate the set of optional classes

            // TODO: populate the set of optional genders

            // TODO: get limits on name length

            _newCharacterController = NewCharacterController.Create(
                Rpc.Client,
                ModalDialogManager,
                CharacterNameInputField,
                Enumerable.Empty<Toggle>(),
                response.MinimumCharacterNameLength,
                response.MaximumCharacterNameLength);
        }
        #endregion

        #region Event Handlers
        public void CreateCharacterButton_OnClick()
        {
            Debug.Log("Create character clicked.");
            _newCharacterController.TryCreateCharacter();
        }

        public void CancelButton_OnClick()
        {
            Debug.Log("Cancel clicked.");
            Application.LoadLevel(0);
        }
        #endregion
    }
}