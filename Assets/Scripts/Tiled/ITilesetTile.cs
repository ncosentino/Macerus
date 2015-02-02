using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Tiled
{
    public interface ITilesetTile
    {
        #region Properties
        int Id { get; }
        
        IEnumerable<string>  PropertyNames { get; }
        #endregion

        #region Methods
        string GetProperty(string propertyName);
        #endregion
    }
}
