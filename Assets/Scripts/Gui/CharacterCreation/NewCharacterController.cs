using System;
using System.Collections.Generic;
using Assets.Scripts.Api;
using Assets.Scripts.Gui.MessageBoxes;
using ProjectXyz.Api.Messaging.Core.CharacterCreation;
using ProjectXyz.Api.Messaging.Core.General;
using ProjectXyz.Api.Messaging.Core.Initialization;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Gui
{
    public sealed class NewCharacterController : INewCharacterController
    {
        #region Fields
        private readonly IRpcClient _rpcClient;
        private readonly IModalDialogManagerBehaviour _modalDialogManagerBehaviour;
        private readonly InputField _characterNameInputField;
        private readonly List<Toggle> _genderToggles;
        private readonly int _minimumCharacterName;
        private readonly int _maximumCharacterName;
        #endregion

        #region Constructors
        private NewCharacterController(
            IRpcClient rpcClient,
            IModalDialogManagerBehaviour modalDialogManagerBehaviour,
            InputField characterNameInputField,
            IEnumerable<Toggle> genderToggles,
            int minimumCharacterName,
            int maximumCharacterName)
        {
            _rpcClient = rpcClient;
            _modalDialogManagerBehaviour = modalDialogManagerBehaviour;
            _characterNameInputField = characterNameInputField;
            _genderToggles = new List<Toggle>(genderToggles);
            _minimumCharacterName = minimumCharacterName;
            _maximumCharacterName = maximumCharacterName;
        }
        #endregion

        #region Methods
        public static INewCharacterController Create(
            IRpcClient rpcClient,
            IModalDialogManagerBehaviour modalDialogManagerBehaviour,
            InputField characterNameInputField,
            IEnumerable<Toggle> genderToggles,
            int minimumCharacterName,
            int maximumCharacterName)
        {
            var controller = new NewCharacterController(
                rpcClient,
                modalDialogManagerBehaviour,
                characterNameInputField,
                genderToggles,
                minimumCharacterName,
                maximumCharacterName);
            return controller;
        }

        public bool TryCreateCharacter()
        {
            if (_characterNameInputField.text == null ||
                _characterNameInputField.text.Length < _minimumCharacterName || 
                _characterNameInputField.text.Length > _maximumCharacterName)
            {
                _modalDialogManagerBehaviour.ShowMessage(
                    string.Format(
                        "Please enter a name for the character between {0} and {1} characters in length.",
                        _minimumCharacterName,
                        _maximumCharacterName),
                    _ =>
                    {
                        _characterNameInputField.ActivateInputField();
                    });
                return false;
            }

            // TODO: pull race information

            // TODO: pull class information

            // TODO: pull gender information

            BooleanResultResponse createResponse;
            if (!_rpcClient.TrySend(
                new CreateCharacterRequest()
                {
                    Name = _characterNameInputField.text,
                    Male = true,
                    RaceId = Guid.NewGuid(),
                    ClassId = Guid.NewGuid(),
                },
                TimeSpan.FromSeconds(5),
                out createResponse) ||
                !createResponse.Result)
            {
                _modalDialogManagerBehaviour.ShowMessage("The character could not be created.");
                return false;
            }

            BooleanResultResponse initializeResponse;
            if (!_rpcClient.TrySend(
                new InitializeWorldRequest()
                {
                    Id = Guid.NewGuid(),
                    PlayerId = Guid.NewGuid(),
                },
                TimeSpan.FromSeconds(5),
                out initializeResponse) ||
                !initializeResponse.Result)
            {
                _modalDialogManagerBehaviour.ShowMessage("The world could not be initialized.");
                return false;
            }

            Application.LoadLevel(2);
            return true;
        }
        #endregion
    }
}