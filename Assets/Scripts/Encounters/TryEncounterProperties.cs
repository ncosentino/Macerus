using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Encounters
{
    public class TryEncounterProperties : ITryEncounterProperties
    {
        #region Fields
        private readonly ICanEncounter _encounterer;
        #endregion

        #region Constructors
        public TryEncounterProperties(ICanEncounter encounterer)
        {
            _encounterer = encounterer;
        }
        #endregion

        #region Properties
        public ICanEncounter Encounterer
        {
            get { return _encounterer; }
        }
        #endregion
    }
}
