using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Maps
{
    public interface IMapLoader
    {
        #region Methods
        void LoadMap(ILoadMapProperties loadMapProperties);
        #endregion
    }
}
