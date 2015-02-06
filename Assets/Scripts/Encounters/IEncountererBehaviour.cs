using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Encounters
{
    public interface IEncountererBehaviour
    {
        #region Methods
        void Encounter(IEncounterProperties encounterProperties);
        #endregion
    }
}
