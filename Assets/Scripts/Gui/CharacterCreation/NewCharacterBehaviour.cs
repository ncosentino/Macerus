using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Gui.MessageBoxes;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Gui.CharacterCreation
{
    public sealed class NewCharacterBehaviour : 
        MonoBehaviour,
        INewCharacterView
    {
        #region Fields
        private readonly List<Toggle> _genderToggles;

        private INewCharacterController _newCharacterController;
        #endregion

        #region Unity Properties
        public RpcBehaviour Rpc;

        public ModalDialogManagerBehaviour ModalDialogManager;

        public InputField CharacterNameInputField;
        #endregion

        #region Constructors
        public NewCharacterBehaviour()
        {
            _genderToggles = new List<Toggle>();
        }
        #endregion

        #region Events
        public event EventHandler<EventArgs> Create;

        public event EventHandler<EventArgs> Cancel;
        #endregion

        #region Properties
        public string CharacterName
        {
            get { return CharacterNameInputField.text; }
        }

        public Guid SelectedGenderId
        {
            // TODO: look for the selected toggle...
            get { return Guid.NewGuid(); }
        }

        public Guid SelectedRaceId
        {
            // TODO: look for the selected race...
            get { return Guid.NewGuid(); }
        }

        public Guid SelectedClassId
        {
            // TODO: look for the selected class...
            get { return Guid.NewGuid(); }
        }
        #endregion

        #region Methods
        private void Start()
        {
            _newCharacterController = NewCharacterController.Create(
                Rpc.Client,
                ModalDialogManager,
                this);
            _newCharacterController.Initialize();
        }

        private void Destroy()
        {
            _newCharacterController.Dispose();
        }
        #endregion

        #region Event Handlers
        public void CreateCharacterButton_OnClick()
        {
            var handler = Create;
            if (handler != null)
            {
                handler.Invoke(this, EventArgs.Empty);
            }
        }

        public void CancelButton_OnClick()
        {
            var handler = Cancel;
            if (handler != null)
            {
                handler.Invoke(this, EventArgs.Empty);
            }
        }

        public void FocusCharacterName()
        {
            CharacterNameInputField.ActivateInputField();
        }

        public void SetGenderOptions(IEnumerable<KeyValuePair<Guid, string>> options)
        {
            _genderToggles.Clear();

            // TODO: populate the toggle controls instead of finding existing ones...
            foreach (var toggle in gameObject
                .GetComponentsInChildren<Toggle>()
                .Where(x => x.name.IndexOf("Gender", StringComparison.OrdinalIgnoreCase) != -1))
            {
                _genderToggles.Add(toggle);
            }
        }

        public void SetRaceOptions(IEnumerable<KeyValuePair<Guid, string>> options)
        {
            // TODO: set the race options in the UI
        }

        public void SetClassOptions(IEnumerable<KeyValuePair<Guid, string>> options)
        {
            // TODO: set the race options in the UI
        }
        #endregion
    }
}