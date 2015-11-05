using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Maps;
using ProjectXyz.Game.Interface;

namespace Assets.Scripts.Maps
{
    public interface ILoadMapProperties
    {
        #region Properties
        IGameManager Manager { get; }

        IMapContext MapContext { get; }

        Guid MapId { get; }
        #endregion
    }
}
