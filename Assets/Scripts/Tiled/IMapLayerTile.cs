using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Tiled
{
    public interface IMapLayerTile
    {
        #region Properties
        int Gid { get; }
        #endregion
    }
}
