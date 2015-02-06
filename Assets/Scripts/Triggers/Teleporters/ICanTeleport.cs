using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Triggers.Teleporters
{
    public interface ICanTeleport
    {
        #region Methods
        void Teleport(ITeleportProperties teleportProperties);
        #endregion
    }
}
