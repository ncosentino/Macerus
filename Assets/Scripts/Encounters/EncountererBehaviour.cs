using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Components;
using UnityEngine;

namespace Assets.Scripts.Encounters
{
    public class EncountererBehaviour : MonoBehaviour
    {
        #region Constants
        private const float INTERVAL = 2;
        #endregion

        #region Fields
        private float _remainingTime;
        private ICanEncounter _encounterer;
        #endregion

        #region Constructors
        #endregion

        #region Methods
        public void Start()
        {
            _encounterer = this.GetRequiredComponent<ICanEncounter>();
        }

        public void OnTriggerStay2D(Collider2D collider)
        {
            _remainingTime -= Time.deltaTime;
            if (_remainingTime > 0)
            {
                return;
            }

            _remainingTime = INTERVAL;
            Debug.Log("Encounterer is trying to encounter.");

            var tryEncounterProperties = new TryEncounterProperties(_encounterer);

            collider.gameObject.SendMessage(
                "TryEncounter",
                tryEncounterProperties,
                SendMessageOptions.DontRequireReceiver);
        }
        #endregion
    }
}