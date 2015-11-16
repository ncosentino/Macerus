using System;

namespace Assets.Scripts.Gui.CharacterCreation
{
    public interface INewCharacterController : IDisposable
    {
        #region Methods
        void Initialize();

        bool TryCreateCharacter();
        #endregion
    }
}