using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Tiled
{
    public class TilesetTile : ITilesetTile
    {
        #region Fields
        private readonly int _id;
        private readonly Dictionary<string, string> _properties;
        #endregion

        #region Constructors
        public TilesetTile(int id, IEnumerable<KeyValuePair<string, string>> properties)
        {
            _id = id;
            _properties = new Dictionary<string, string>();

            foreach (var entry in properties)
            {
                _properties[entry.Key] = entry.Value;
            }
        }
        #endregion

        #region Properties
        public int Id
        {
            get { return _id; }
        }

        public IEnumerable<string> PropertyNames
        {
            get { return _properties.Keys; }
        }

        public IEnumerable<KeyValuePair<string, string>> Properties
        {
            get { return _properties; }
        }
        #endregion

        #region Methods
        public string GetPropertyValue(string propertyName)
        {
            return _properties.ContainsKey(propertyName)
                ? _properties[propertyName]
                : null;
        }
        #endregion
    }
}
