using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Maps.Tiled
{
    public class MapLayer : IMapLayer
    {
        #region Fields
        private readonly string _name;
        private readonly int _width;
        private readonly int _heighth;
        private readonly IDictionary<int, IDictionary<int, MapLayerTile>> _tiles;
        #endregion

        #region Constructors
        public MapLayer(string name, int width, int height, IDictionary<int, IDictionary<int, MapLayerTile>> tiles)
        {
            _name = name;
            _width = width;
            _heighth = height;
            _tiles = tiles; // FIXME: should probably create a copy...
        }
        #endregion

        #region Properties
        public string Name
        {
            get { return _name; }
        }

        public int Width
        {
            get { return _width; }
        }

        public int Heighth
        {
            get { return _heighth; }
        }
        #endregion

        #region Methods
        public MapLayerTile GetTile(int x, int y)
        {
            return _tiles[y][x];
        }
        #endregion
    }
}
