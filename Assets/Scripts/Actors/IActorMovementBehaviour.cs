using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Actors
{
    public interface IActorMovementBehaviour : IActorMovementState
    {
        #region Methods
        void Move(ActorDirection direction);

        void Idle();
        #endregion
    }
}