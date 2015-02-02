using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Maps.Tiled
{
    public interface IMapLayerTile
    {
        #region Properties
        int Gid { get; }
        #endregion
    }
}
