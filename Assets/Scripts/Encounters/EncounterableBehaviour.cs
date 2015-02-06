using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Encounters
{
    public class EncounterableBehaviour : MonoBehaviour, IEncounterableBehaviour
    {
        #region Constants
        private const float CHANCE = 0.5f;
        #endregion

        #region Fields
        #endregion

        #region Constructors
        #endregion

        #region Methods
        public void TryEncounter(ITryEncounterProperties tryEncounterProperties)
        {
            Debug.Log("Encounterable may be encountered...");
            if (UnityEngine.Random.value > CHANCE)
            {
                return;
            }

            Debug.Log("Encounterable has being encountered.");
            var encounterProperties = new EncounterProperties();

            tryEncounterProperties.Encounterer.SendMessage(
                "Encounter",
                encounterProperties,
                SendMessageOptions.RequireReceiver);
        }
        #endregion
    }
}