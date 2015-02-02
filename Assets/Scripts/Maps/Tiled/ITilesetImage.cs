using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Maps.Tiled
{
    public interface ITilesetImage
    {
        #region Properties
        string SourcePath { get; }
        
        int Width { get; }
        
        int Height { get; }
        #endregion
    }
}
