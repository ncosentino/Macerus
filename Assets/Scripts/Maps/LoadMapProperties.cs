using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Maps;

namespace Assets.Scripts.Maps
{
    public class LoadMapProperties : ILoadMapProperties
    {
        #region Fields
        private readonly IMapContext _mapContext;
        private readonly IManager _manager;
        private readonly Guid _mapId;
        #endregion

        #region Constructors
        public LoadMapProperties(IManager manager, IMapContext mapContext, Guid mapId)
        {
            _manager = manager;
            _mapContext = mapContext;
            _mapId = mapId;
        }
        #endregion

        #region Properties
        public Guid MapId
        {
            get { return _mapId; }
        }

        public IManager Manager
        {
            get { return _manager; }
        }

        public IMapContext MapContext
        {
            get { return _mapContext; }
        }
        #endregion
    }
}
