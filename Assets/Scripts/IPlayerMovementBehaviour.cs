using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public interface IPlayerMovementBehaviour : IPlayerMovementState
    {
        #region Methods
        void Move(PlayerDirection direction);

        void Idle();
        #endregion
    }
}