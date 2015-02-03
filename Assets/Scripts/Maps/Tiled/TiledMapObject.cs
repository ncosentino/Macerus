using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Maps.Tiled
{
    public class TiledMapObject : ITiledMapObject
    {
        #region Fields
        private readonly string _id;
        private readonly string _gid;
        private readonly string _name;
        private readonly string _type;
        private readonly float _x;
        private readonly float _y;
        private readonly float? _width;
        private readonly float? _height;
        private readonly Dictionary<string, string> _properties;
        #endregion

        #region Constructors
        public TiledMapObject(string id, string name, string type, string gid, float x, float y, float? width, float? height, IEnumerable<KeyValuePair<string, string>> properties)
        {
            _id = id;
            _name = name;
            _type = type;
            _gid = gid;
            _x = x;
            _y = y;
            _width = width;
            _height = height;
            _properties = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            foreach (var property in properties)
            {
                _properties[property.Key] = property.Value;
            }
        }
        #endregion

        #region Properties
        public string Id
        {
            get { return _id; }
        }

        public string Gid
        {
            get { return _gid; }
        }

        public string Name
        {
            get { return _name; }
        }

        public string Type
        {
            get { return _type; }
        }

        public float X
        {
            get { return _x; }
        }

        public float Y
        {
            get { return _y; }
        }

        public float? Width
        {
            get { return _width; }
        }

        public float? Height
        {
            get { return _height; }
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
