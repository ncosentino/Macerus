using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Tiled
{
    public interface IMapLayer
    {
        #region Properties
        string Name { get; }
        
        int Width { get; }
        
        int Heighth { get; }
        #endregion

        #region Methods
        MapLayerTile GetTile(int x, int y);
        #endregion
    }
}
