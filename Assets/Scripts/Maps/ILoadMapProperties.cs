using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Maps;

namespace Assets.Scripts.Maps
{
    public interface ILoadMapProperties
    {
        #region Properties
        IManager Manager { get; }

        IMapContext MapContext { get; }

        Guid MapId { get; }
        #endregion
    }
}
