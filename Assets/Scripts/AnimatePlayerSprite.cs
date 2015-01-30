using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class AnimatePlayerSprite : MonoBehaviour
    {
        #region Constants
        private static readonly Dictionary<PlayerDirection, int> DIRECTION_TO_ANIMATION_INDEX = new Dictionary<PlayerDirection, int>()
        {
            { PlayerDirection.Down, 0 },
            { PlayerDirection.Up, 1 },
            { PlayerDirection.Right, 2 },
            { PlayerDirection.Left, 3 },
        };
        #endregion

        #region Fields
        private IPlayerMovementState _playerMovementState;
        private Animator _animator;
        #endregion
        
        #region Methods
        public void Start()
        {
            _animator = GetComponent<Animator>();
            _playerMovementState = GetComponent<PlayerMovementBehaviour>();
        }

        public void Update()
        {
            if (_playerMovementState.Speed > 0f)
            {
                _animator.SetBool("Walking", true);
                _animator.SetInteger("Direction", DIRECTION_TO_ANIMATION_INDEX[_playerMovementState.Direction]);
            }
            else
            {
                _animator.SetBool("Walking", false);
            }
        }
        #endregion
    }
}