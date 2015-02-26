using System;

namespace Assets.Scripts.Interactables.Teleporters
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
