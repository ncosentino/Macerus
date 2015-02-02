using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Maps.Tiled
{
    public interface IObjectLayer
    {
        #region Properties
        string Name { get; }

        IEnumerable<MapObject> Objects { get; }
        #endregion
    }
}
