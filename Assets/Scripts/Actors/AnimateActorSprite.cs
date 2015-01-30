using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Actors
{
    public class AnimateActorSprite : MonoBehaviour
    {
        #region Constants
        private static readonly Dictionary<ActorDirection, int> DIRECTION_TO_ANIMATION_INDEX = new Dictionary<ActorDirection, int>()
        {
            { ActorDirection.Down, 0 },
            { ActorDirection.Up, 1 },
            { ActorDirection.Right, 2 },
            { ActorDirection.Left, 3 },
        };
        #endregion

        #region Fields
        private IActorMovementState _actorMovementState;
        private Animator _animator;
        #endregion
        
        #region Methods
        public void Start()
        {
            _animator = GetComponent<Animator>();
            _actorMovementState = GetComponent<ActorMovementBehaviour>();
        }

        public void Update()
        {
            if (_actorMovementState.Speed > 0f)
            {
                _animator.SetBool("Walking", true);
                _animator.SetInteger("Direction", DIRECTION_TO_ANIMATION_INDEX[_actorMovementState.Direction]);
            }
            else
            {
                _animator.SetBool("Walking", false);
            }
        }
        #endregion
    }
}