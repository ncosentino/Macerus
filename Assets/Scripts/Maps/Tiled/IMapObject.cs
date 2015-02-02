using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Maps.Tiled
{
    public interface IMapObject
    {
        #region Fields
        string Id { get; }

        string Gid { get; }
        
        string Name { get; }
        
        string Type { get; }
        
        int X { get; }
        
        int Y { get; }
        #endregion
    }
}
