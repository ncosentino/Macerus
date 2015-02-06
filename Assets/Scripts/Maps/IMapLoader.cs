using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Maps
{
    public interface IMapLoader
    {
        #region Methods
        void LoadMap(ILoadMapProperties loadMapProperties);
        #endregion
    }
}
