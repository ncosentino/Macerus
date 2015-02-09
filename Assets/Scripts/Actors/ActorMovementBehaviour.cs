using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Actors
{
    [RequireComponent(typeof(IActorBehaviour))]
    public class ActorMovementBehaviour : MonoBehaviour, IActorMovementBehaviour
    {
        #region Fields
        private IActorBehaviour _actorBehaviour;
        #endregion

        #region Properties
        public float Speed { get; private set; }

        public ActorDirection Direction { get; private set; }
        #endregion

        #region Methods
        public void Start()
        {
            _actorBehaviour = (IActorBehaviour)gameObject.GetComponent(typeof(IActorBehaviour));
        }

        public void Update()
        {
            var deltaTimeSeconds = Time.deltaTime;
            if (Speed <= 0)
            {
                return;
            }

            var distance = Speed * deltaTimeSeconds;

            switch (Direction)
            {
                case ActorDirection.Down:
                    _actorBehaviour.ActorGameObject.transform.Translate(0, -distance, 0);
                    break;

                case ActorDirection.Up:
                    _actorBehaviour.ActorGameObject.transform.Translate(0, distance, 0);
                    break;

                case ActorDirection.Right:
                    _actorBehaviour.ActorGameObject.transform.Translate(distance, 0, 0);
                    break;

                case ActorDirection.Left:
                    _actorBehaviour.ActorGameObject.transform.Translate(-distance, 0, 0);
                    break;
            }

            _actorBehaviour.Actor.UpdatePosition(
                UnityToGlobalCoordinates(_actorBehaviour.ActorGameObject.transform.position.x),
                UnityToGlobalCoordinates(_actorBehaviour.ActorGameObject.transform.position.y));
        }

        public void Move(ActorDirection direction)
        {
            Direction = direction;
            Speed = 0.5f;
        }

        public void Idle()
        {
            Speed = 0f;
        }

        private float UnityToGlobalCoordinates(float value)
        {
            return value;
        }
        #endregion
    }
}