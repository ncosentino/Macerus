using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Actors
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(IActorMovementBehaviour))]
    public class AnimateActorSpriteBehaviour : MonoBehaviour
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
        private SpriteRenderer _spriteRenderer;
        #endregion
        
        #region Methods
        public void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            _actorMovementState = (IActorMovementBehaviour)GetComponent(typeof(IActorMovementBehaviour));
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

        public void LateUpdate()
        {
            // little trick to allow 2D sprites to go in front and behind each other
            const int PIXEL_OFFSET = 10;
            _spriteRenderer.sortingOrder = (int)((UnityEngine.Camera.main.WorldToScreenPoint(_spriteRenderer.bounds.min).y + PIXEL_OFFSET) * -10);
        }
        #endregion
    }
}