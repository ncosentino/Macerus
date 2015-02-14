﻿using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Components;
using UnityEngine;

namespace Assets.Scripts.Actors.Player
{
    public class PlayerInputControllerBehaviour : MonoBehaviour, IPlayerInputControllerBehaviour
    {
        #region Fields
        private IPlayerControlsBehaviour _playerControls;
        private IActorMovementBehaviour _actorMovementBehaviour;
        #endregion

        #region Methods
        public void Start()
        {
            _playerControls = this.GetRequiredComponent<IPlayerControlsBehaviour>();
            _actorMovementBehaviour = this.GetRequiredComponent<IActorMovementBehaviour>();
        }

        public void Update()
        {
            if (Input.GetKey(_playerControls.MoveDown))
            {
                _actorMovementBehaviour.Move(ActorDirection.Down);
            }
            else if (Input.GetKey(_playerControls.MoveUp))
            {
                _actorMovementBehaviour.Move(ActorDirection.Up);
            }
            else if (Input.GetKey(_playerControls.MoveRight))
            {
                _actorMovementBehaviour.Move(ActorDirection.Right);
            }
            else if (Input.GetKey(_playerControls.MoveLeft))
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
