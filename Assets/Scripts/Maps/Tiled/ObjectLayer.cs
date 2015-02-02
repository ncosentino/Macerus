using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Maps.Tiled
{
    public class ObjectLayer : IObjectLayer
    {
        #region Fields
        private readonly string _name;
        private readonly List<MapObject> _objects;
        #endregion

        #region Constructors
        public ObjectLayer(string name, IEnumerable<MapObject> objects)
        {
            _name = name;
            _objects = new List<MapObject>(objects);
        }
        #endregion

        #region Properties
        public string Name
        {
            get { return _name; }
        }

        public IEnumerable<MapObject> Objects
        {
            get { return _objects; }
        }
        #endregion
    }
}
