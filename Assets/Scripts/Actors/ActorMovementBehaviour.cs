using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Actors;
using UnityEngine;

namespace Assets.Scripts
{
    public class ActorMovementBehaviour : MonoBehaviour, IActorMovementBehaviour
    {
        #region Properties
        public float Speed { get; private set; }

        public ActorDirection Direction { get; private set; }
        #endregion

        #region Methods
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
                    gameObject.transform.Translate(0, -distance, 0);
                    break;

                case ActorDirection.Up:
                    gameObject.transform.Translate(0, distance, 0);
                    break;

                case ActorDirection.Right:
                    gameObject.transform.Translate(distance, 0, 0);
                    break;

                case ActorDirection.Left:
                    gameObject.transform.Translate(-distance, 0, 0);
                    break;
            }
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
        #endregion
    }
}