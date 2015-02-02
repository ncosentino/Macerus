using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Tiled
{
    public class TilesetTile
    {
        #region Fields
        private readonly string _id;
        private readonly Dictionary<string, string> _properties;
        #endregion

        #region Constructors
        public TilesetTile(string id, IEnumerable<KeyValuePair<string, string>> properties)
        {
            _id = id;
            _properties = new Dictionary<string, string>();

            foreach (var entry in properties)
            {
                _properties[entry.Key] = entry.Value;
            }
        }
        #endregion
    }
}
