using System;
using System.Collections.Generic;

namespace Assets.Scripts.Gui.CharacterCreation
{
    public interface INewCharacterView
    {
        #region Events
        event EventHandler<EventArgs> Create;

        event EventHandler<EventArgs> Cancel;
        #endregion

        #region Properties
        string CharacterName { get; }

        Guid SelectedGenderId { get; }

        Guid SelectedRaceId { get; }

        Guid SelectedClassId { get; }
        #endregion

        #region Methods
        void FocusCharacterName();

        void SetGenderOptions(IEnumerable<KeyValuePair<Guid, string>> options);

        void SetRaceOptions(IEnumerable<KeyValuePair<Guid, string>> options);

        void SetClassOptions(IEnumerable<KeyValuePair<Guid, string>> options);
        #endregion
    }
}