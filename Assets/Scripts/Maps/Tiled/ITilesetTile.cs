using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Maps.Tiled
{
    public interface ITilesetTile
    {
        #region Properties
        int Id { get; }
        
        IEnumerable<string>  PropertyNames { get; }
        
        IEnumerable<KeyValuePair<string, string>> Properties { get; }
        #endregion

        #region Methods
        string GetPropertyValue(string propertyName);
        #endregion
    }
}
