using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Actors;

namespace Assets.Scripts.Scenes.Explore.Input
{
    public class PlayerInputController : IPlayerInputController
    {
        #region Fields
        private readonly IActorMovementBehaviour _actorMovementBehaviour;
        private readonly IKeyboardControls _keyboardControls;
        #endregion

        #region Constructors
        public PlayerInputController(IKeyboardControls keyboardControls, IActorMovementBehaviour actorMovementBehaviour)
        {
            _keyboardControls = keyboardControls;
            _actorMovementBehaviour = actorMovementBehaviour;
        }
        #endregion
        
        #region Methods
        public void Update(float deltaTime)
        {
            if (UnityEngine.Input.GetKey(_keyboardControls.MoveDown))
            {
                _actorMovementBehaviour.Move(ActorDirection.Down);
            }
            else if (UnityEngine.Input.GetKey(_keyboardControls.MoveUp))
            {
                _actorMovementBehaviour.Move(ActorDirection.Up);
            }
            else if (UnityEngine.Input.GetKey(_keyboardControls.MoveRight))
            {
                _actorMovementBehaviour.Move(ActorDirection.Right);
            }
            else if (UnityEngine.Input.GetKey(_keyboardControls.MoveLeft))
            {
                _actorMovementBehaviour.Move(ActorDirection.Left);
            }
            else
            {
                _actorMovementBehaviour.Idle();
            }
        }
        #endregion
    }
}
