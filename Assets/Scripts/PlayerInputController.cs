using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerInputController : MonoBehaviour, IPlayerInputController
    {
        #region Fields
        private IPlayerControls _playerControls;
        private IPlayerMovementBehaviour _playerMovementBehaviour;
        #endregion

        #region Methods
        public void Start()
        {
            _playerControls = GetComponent<PlayerControls>();
            _playerMovementBehaviour = GetComponent<PlayerMovementBehaviour>();
        }

        public void Update()
        {
            if (Input.GetKey(_playerControls.MoveDown))
            {
                _playerMovementBehaviour.Move(PlayerDirection.Down);
            }
            else if (Input.GetKey(_playerControls.MoveUp))
            {
                _playerMovementBehaviour.Move(PlayerDirection.Up);
            }
            else if (Input.GetKey(_playerControls.MoveRight))
            {
                _playerMovementBehaviour.Move(PlayerDirection.Right);
            }
            else if (Input.GetKey(_playerControls.MoveLeft))
            {
                _playerMovementBehaviour.Move(PlayerDirection.Left);
            }
            else
            {
                _playerMovementBehaviour.Idle();
            }
        }
        #endregion
    }
}
