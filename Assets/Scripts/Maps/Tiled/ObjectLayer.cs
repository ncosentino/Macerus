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
        private readonly List<TiledMapObject> _objects;
        #endregion

        #region Constructors
        public ObjectLayer(string name, IEnumerable<TiledMapObject> objects)
        {
            _name = name;
            _objects = new List<TiledMapObject>(objects);
        }
        #endregion

        #region Properties
        public string Name
        {
            get { return _name; }
        }

        public IEnumerable<TiledMapObject> Objects
        {
            get { return _objects; }
        }
        #endregion
    }
}
