using System.Linq;
using Assets.Scripts.Actors;
using Assets.Scripts.Actors.Player;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Input
{
    public class PlayerInputController : IPlayerInputController
    {
        #region Fields
        private readonly IPlayerBehaviour _playerBehaviour;
        private readonly IActorMovementBehaviour _actorMovementBehaviour;
        private readonly IKeyboardControls _keyboardControls;
        #endregion

        #region Constructors
        public PlayerInputController(IKeyboardControls keyboardControls, IActorMovementBehaviour actorMovementBehaviour, IPlayerBehaviour playerBehaviour)
        {
            _keyboardControls = keyboardControls;
            _actorMovementBehaviour = actorMovementBehaviour;
            _playerBehaviour = playerBehaviour;
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

            if (UnityEngine.Input.GetKeyUp(_keyboardControls.Interact))
            {
                var interaction = _playerBehaviour.Interactables
                    .SelectMany(x => x.GetPossibleInteractions(null, _playerBehaviour.Player))
                    .FirstOrDefault();
                interaction.Interact(null, _playerBehaviour.Player);
                Debug.Log(interaction.ToString());
            }
        }
        #endregion
    }
}
