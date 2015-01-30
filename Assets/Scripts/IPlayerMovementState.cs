using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public interface IPlayerMovementState
    {
        #region Properties
        float Speed { get; }

        PlayerDirection Direction { get; }
        #endregion
    }
}