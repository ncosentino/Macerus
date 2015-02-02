using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Tiled
{
    public class TiledMap
    {
        #region Fields
        private readonly int _width;
        private readonly int _height;
        private readonly int _tileWidth;
        private readonly int _tileHeight;
        private readonly List<Tileset> _tilesets;
        #endregion

        #region Constructors
        public TiledMap(int width, int height, int tileWidth, int tileHeight, IEnumerable<Tileset> tilesets)
        {
            _width = width;
            _height = height;
            _tileWidth = tileWidth;
            _tileHeight = tileHeight;
            _tilesets = new List<Tileset>(tilesets);
        }
        #endregion
    }
}
