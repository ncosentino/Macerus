using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Actors.Player
{
    public class PlayerBehaviourRegistrar : IPlayerBehaviourRegistrar
    {
        #region Fields
        private IPlayerBehaviour _player;
        #endregion

        #region Events
        public event EventHandler<PlayerBehaviourRegisteredEventArgs> PlayerRegistered;

        public event EventHandler<PlayerBehaviourRegisteredEventArgs> PlayerUnregistered;
        #endregion

        #region Methods
        public void UnregisterPlayer(IPlayerBehaviour player)
        {
            if (player != _player)
            {
                return;
            }

            _player = null;

            var handler = PlayerUnregistered;
            if (handler != null)
            {
                handler.Invoke(this, new PlayerBehaviourRegisteredEventArgs(player));
            }
        }

        public void RegisterPlayer(IPlayerBehaviour player)
        {
            if (player == _player || player == null)
            {
                return;
            }

            _player = player;

            var handler = PlayerRegistered;
            if (handler != null)
            {
                handler.Invoke(this, new PlayerBehaviourRegisteredEventArgs(player));
            }
        }
        #endregion
    }
}
