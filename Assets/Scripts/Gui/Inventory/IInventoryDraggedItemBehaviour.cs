using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Gui.Inventory
{
    public interface IInventoryDraggedItemBehaviour : IDragHandler, IEndDragHandler
    {
        #region Properties
        IInventoryDragItemSourceBehaviour Source { get; set; }

        IHasItemBehaviour HasItemBehaviour { get; set; }

        Sprite Icon { get; set; }
        #endregion
    }
}
