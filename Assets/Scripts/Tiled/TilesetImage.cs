using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Tiled
{
    public class TilesetImage : ITilesetImage
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

        #region Properties
        public string SourcePath
        {
            get { return _sourcePath; }
        }

        public int Width
        {
            get { return _width; }
        }

        public int Height
        {
            get { return _height; }
        }
        #endregion
    }
}
