using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Tiled
{
    public class MapLayerTile : IMapLayerTile
    {
        #region Fields
        private readonly int _gid;
        #endregion

        #region Constructors
        public MapLayerTile(int gid)
        {
            _gid = gid;
        }
        #endregion

        #region Properties
        public int Gid
        {
            get { return _gid; }
        }
        #endregion
    }
}
