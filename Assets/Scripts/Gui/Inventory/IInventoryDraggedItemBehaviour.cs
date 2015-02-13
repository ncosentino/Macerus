using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Gui.Inventory
{
    public interface IInventoryDraggedItemBehaviour : IDragHandler, IEndDragHandler
    {
        #region Properties
        IInventoryDragItemSourceBehaviour Source { get; set; }

        IHasItemBehaviour HasItemBehaviour { get; set; }
        #endregion
    }
}
