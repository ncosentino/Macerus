using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Triggers.Teleporters
{
    public class TeleportProperties : ITeleportProperties
    {
        #region Fields
        private readonly Guid _mapId;
        #endregion

        #region Constructors
        public TeleportProperties(Guid mapId)
        {
            _mapId = mapId;
        }
        #endregion

        #region Properties
        public Guid MapId
        {
            get { return _mapId; }
        }
        #endregion
    }
}
