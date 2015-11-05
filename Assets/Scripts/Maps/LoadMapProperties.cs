using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Maps;
using ProjectXyz.Game.Interface;

namespace Assets.Scripts.Maps
{
    public class LoadMapProperties : ILoadMapProperties
    {
        #region Fields
        private readonly IMapContext _mapContext;
        private readonly IGameManager _manager;
        private readonly Guid _mapId;
        #endregion

        #region Constructors
        public LoadMapProperties(IGameManager manager, IMapContext mapContext, Guid mapId)
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

        public IGameManager Manager
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
