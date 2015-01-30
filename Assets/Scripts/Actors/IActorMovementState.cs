using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Actors
{
    public interface IActorMovementState
    {
        #region Properties
        float Speed { get; }

        ActorDirection Direction { get; }
        #endregion
    }
}