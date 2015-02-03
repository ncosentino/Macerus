using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Encounters
{
    public class EncounterBehaviour : MonoBehaviour
    {
        #region Constants
        private const float MINIMUM_TIME = 2;
        private const float INTERVAL = 2;
        private const float CHANCE = 0.5f;
        #endregion

        #region Fields
        private float _timeRemaining;
        #endregion

        #region Methods
        public void OnTriggerEnter2D(Collider2D collider)
        {
            _timeRemaining = MINIMUM_TIME;
        }

        public void OnTriggerStay2D(Collider2D collider)
        {
            _timeRemaining -= Time.deltaTime;

            if (_timeRemaining <= 0)
            {
                if (UnityEngine.Random.value <= CHANCE)
                {
                    Debug.Log("Encounter!");
                }

                _timeRemaining = INTERVAL;
            }
        }
        #endregion
    }
}