using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Triggers.Teleporters
{
    public interface ITeleportProperties
    {
        #region Properties
        Guid MapId { get; }
        #endregion
    }
}
