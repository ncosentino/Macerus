using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Api;
using Assets.Scripts.Gui.MessageBoxes;
using ProjectXyz.Api.Messaging.Core.CharacterCreation;
using ProjectXyz.Api.Messaging.Core.General;
using ProjectXyz.Api.Messaging.Core.Initialization;
using UnityEngine;

namespace Assets.Scripts.Gui.CharacterCreation
{
    public sealed class NewCharacterController : INewCharacterController
    {
        #region Fields
        private readonly IRpcClient _rpcClient;
        private readonly IModalDialogManagerBehaviour _modalDialogManagerBehaviour;
        private readonly INewCharacterView _newCharacterView;

        private int _minimumCharacterNameLength;
        private int _maximumCharacterNameLength;
        #endregion

        #region Constructors
        private NewCharacterController(
            IRpcClient rpcClient,
            IModalDialogManagerBehaviour modalDialogManagerBehaviour,
            INewCharacterView newCharacterView)
        {
            _rpcClient = rpcClient;
            _modalDialogManagerBehaviour = modalDialogManagerBehaviour;

            _newCharacterView = newCharacterView;
            _newCharacterView.Create += NewCharacterView_Create;
            _newCharacterView.Cancel += NewCharacterView_Cancel;
        }

        ~NewCharacterController()
        {
            Dispose(false);
        }
        #endregion

        #region Methods
        public static INewCharacterController Create(
            IRpcClient rpcClient,
            IModalDialogManagerBehaviour modalDialogManagerBehaviour,
            INewCharacterView newCharacterView)
        {
            var controller = new NewCharacterController(
                rpcClient,
                modalDialogManagerBehaviour,
                newCharacterView);
            return controller;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Initialize()
        {
            GetNewCharacterOptionsResponse response;
            if (!_rpcClient.TrySend(
                new GetNewCharacterOptionsRequest(),
                TimeSpan.FromSeconds(5),
                out response))
            {
                _modalDialogManagerBehaviour.ShowMessage("Could not get the character creation options.");
                return;
            }

            _newCharacterView.SetRaceOptions(response
                .Races
                .Select(x => new KeyValuePair<Guid, string>(
                    x.RaceId,
                    // TODO: look up this resource string somehow...
                    x.NameStringResourceId.ToString())));
            _newCharacterView.SetClassOptions(response
                .Classes
                .Select(x => new KeyValuePair<Guid, string>(
                    x.ClassId,
                    // TODO: look up this resource string somehow...
                    x.NameStringResourceId.ToString())));
            _newCharacterView.SetRaceOptions(response
                .Genders
                .Select(x => new KeyValuePair<Guid, string>(
                    x.GenderId,
                    // TODO: look up this resource string somehow...
                    x.NameStringResourceId.ToString())));

            _minimumCharacterNameLength = response.MinimumCharacterNameLength;
            _maximumCharacterNameLength = response.MaximumCharacterNameLength;
        }

        public bool TryCreateCharacter()
        {
            if (_newCharacterView.CharacterName == null ||
                _newCharacterView.CharacterName.Length < _minimumCharacterNameLength ||
                _newCharacterView.CharacterName.Length > _maximumCharacterNameLength)
            {
                _modalDialogManagerBehaviour.ShowMessage(
                    string.Format(
                        "Please enter a name for the character between {0} and {1} characters in length.",
                        _minimumCharacterNameLength,
                        _maximumCharacterNameLength),
                    _ =>
                    {
                        _newCharacterView.FocusCharacterName();
                    });
                return false;
            }
            
            BooleanResultResponse createResponse;
            if (!_rpcClient.TrySend(
                new CreateCharacterRequest()
                {
                    Name = _newCharacterView.CharacterName,
                    GenderId = _newCharacterView.SelectedGenderId,
                    RaceId = _newCharacterView.SelectedRaceId,
                    ClassId = _newCharacterView.SelectedClassId,
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

        private void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            _newCharacterView.Create -= NewCharacterView_Create;
            _newCharacterView.Cancel -= NewCharacterView_Cancel;
        }
        #endregion

        #region Event Handlers
        private void NewCharacterView_Cancel(object sender, EventArgs e)
        {
            Application.LoadLevel(0);
        }

        private void NewCharacterView_Create(object sender, EventArgs e)
        {
            TryCreateCharacter();
        }
        #endregion
    }
}