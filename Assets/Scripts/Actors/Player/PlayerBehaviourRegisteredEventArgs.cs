using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Actors.Player
{
    public class PlayerBehaviourRegisteredEventArgs : EventArgs
    {
        #region Fields
        private readonly IPlayerBehaviour _playerBehaviour;
        #endregion

        #region Constructors
        public PlayerBehaviourRegisteredEventArgs(IPlayerBehaviour playerBehaviour)
        {
            _playerBehaviour = playerBehaviour;
        }
        #endregion

        #region Properties
        public IPlayerBehaviour PlayerBehaviour
        {
            get { return _playerBehaviour; }
        }
        #endregion
    }
}
