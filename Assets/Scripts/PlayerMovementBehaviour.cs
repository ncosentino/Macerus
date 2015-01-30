using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerMovementBehaviour : MonoBehaviour, IPlayerMovementBehaviour
    {
        #region Properties
        public float Speed { get; private set; }

        public PlayerDirection Direction { get; private set; }
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
                case PlayerDirection.Down:
                    gameObject.transform.Translate(0, -distance, 0);
                    break;

                case PlayerDirection.Up:
                    gameObject.transform.Translate(0, distance, 0);
                    break;

                case PlayerDirection.Right:
                    gameObject.transform.Translate(distance, 0, 0);
                    break;

                case PlayerDirection.Left:
                    gameObject.transform.Translate(-distance, 0, 0);
                    break;
            }
        }

        public void Move(PlayerDirection direction)
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