using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Actors.Player
{
    public interface IPlayerBehaviourRegistrar
    {
        #region Events
        event EventHandler<PlayerBehaviourRegisteredEventArgs> PlayerRegistered;

        event EventHandler<PlayerBehaviourRegisteredEventArgs> PlayerUnregistered;
        #endregion
        
        #region Properties
        IPlayerBehaviour PlayerBehaviour { get; }
        #endregion

        #region Methods
        void RegisterPlayer(IPlayerBehaviour player);

        void UnregisterPlayer(IPlayerBehaviour player);
        #endregion
    }
}
