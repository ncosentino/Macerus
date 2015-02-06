using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Encounters
{
    public class EncountererBehaviour : MonoBehaviour, IEncountererBehaviour
    {
        #region Constants
        private const float INTERVAL = 2;
        #endregion

        #region Fields
        private float _remainingTime;
        #endregion

        #region Constructors
        #endregion

        #region Methods
        public void OnTriggerStay2D(Collider2D collider)
        {
            _remainingTime -= Time.deltaTime;
            if (_remainingTime > 0)
            {
                return;
            }

            _remainingTime = INTERVAL;
            Debug.Log("Encounterer is trying to encounter.");

            var tryEncounterProperties = new TryEncounterProperties(gameObject);

            collider.gameObject.SendMessage(
                "TryEncounter",
                tryEncounterProperties,
                SendMessageOptions.DontRequireReceiver);
            Debug.Log("POST: Encounterer is trying to encounter.");
        }

        public void Encounter(IEncounterProperties encounterProperties)
        {
            Debug.Log("Encountered!!!");
        }
        #endregion
    }
}