using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

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

        #region Properties
        public IPlayerBehaviour PlayerBehaviour
        {
            get { return _player; }
        }
        #endregion

        #region Methods
        public void UnregisterPlayer(IPlayerBehaviour player)
        {
            if (player != _player)
            {
                return;
            }

            _player = null;
            Debug.Log("Player has been unregistered.");

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
            Debug.Log("Player has been registered.");

            var handler = PlayerRegistered;
            if (handler != null)
            {
                handler.Invoke(this, new PlayerBehaviourRegisteredEventArgs(player));
            }
        }
        #endregion
    }
}
