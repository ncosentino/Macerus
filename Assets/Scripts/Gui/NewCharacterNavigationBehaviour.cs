using System;
using System.Linq;
using Assets.Scripts.Api;
using Assets.Scripts.Gui.MessageBoxes;
using ProjectXyz.Api.Messaging.Core.General;
using ProjectXyz.Api.Messaging.Core.Initialization;
using ProjectXyz.Api.Messaging.Interface;
using ProjectXyz.Api.Messaging.Serialization.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Gui
{
    public sealed class NewCharacterNavigationBehaviour : MonoBehaviour
    {
        #region Unity Properties
        public RpcBehaviour Rpc;

        public ModalDialogManagerBehaviour ModalDialogManager;
        #endregion

        #region Methods
        private void RequestInitialization()
        {
            var characterNameInputField = GetComponentsInChildren<InputField>()
                .FirstOrDefault(x => x.name == "CharacterNameInputField");

            if (string.IsNullOrEmpty(characterNameInputField.text))
            {
                ModalDialogManager.ShowMessage("Please enter a name for the character.", _ =>
                {
                    characterNameInputField.ActivateInputField();
                });
                return;
            }

            var maleGenderToggle = GetComponentsInChildren<Toggle>()
                .FirstOrDefault(x => x.name == "MaleGenderToggle");

            BooleanResultResponse createResponse;
            if (!Rpc.Client.TrySend(
                new CreateCharacterRequest()
                {
                    Name = characterNameInputField.text,
                    Male = maleGenderToggle.isOn,
                    RaceId = Guid.NewGuid(),
                    ClassId = Guid.NewGuid(),
                },
                TimeSpan.FromSeconds(5),
                out createResponse) ||
                !createResponse.Result)
            {
                ModalDialogManager.ShowMessage("The character could not be created.");
                return;
            }

            BooleanResultResponse initializeResponse;
            if (!Rpc.Client.TrySend(
                new InitializeWorldRequest()
                {
                    Id = Guid.NewGuid(),
                    PlayerId = Guid.NewGuid(),
                }, 
                TimeSpan.FromSeconds(5),
                out initializeResponse) ||
                !initializeResponse.Result)
            {
                ModalDialogManager.ShowMessage("The world could not be initialized.");
                return;
            }
            
            Application.LoadLevel("Explore");
        }
        #endregion

        #region Event Handlers
        public void CreateCharacterButton_OnClick()
        {
            Debug.Log("Create character clicked.");
            RequestInitialization();
        }

        public void CancelButton_OnClick()
        {
            Debug.Log("Cancel clicked.");
            Application.LoadLevel(0);
        }
        #endregion
    }
}