using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Tiled
{
    public class TilesetImage
    {
        #region Fields
        private readonly string _sourcePath;
        private readonly int _width;
        private readonly int _height;
        #endregion

        #region Constructors
        public TilesetImage(string sourcePath, int width, int height)
        {
            _sourcePath = sourcePath;
            _width = width;
            _height = height;
        }
        #endregion
    }
}
