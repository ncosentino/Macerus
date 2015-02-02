using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Tiled
{
    public class MapLayerTile
    {
        #region Fields
        private readonly string _gid;
        #endregion

        #region Constructors
        public MapLayerTile(string gid)
        {
            _gid = gid;
        }
        #endregion
    }
}
