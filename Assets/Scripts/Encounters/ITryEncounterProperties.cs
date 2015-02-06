using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Encounters
{
    public interface ITryEncounterProperties
    {
        #region Properties
        ICanEncounter Encounterer { get; }
        #endregion
    }
}
