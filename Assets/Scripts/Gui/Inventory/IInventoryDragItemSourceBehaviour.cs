using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Gui.Inventory
{
    public interface IInventoryDragItemSourceBehaviour : IBeginDragHandler, IDragHandler
    {
        #region Properties
        GameObject GameObject { get; }

        ICanRemoveItemBehaviour CanRemoveItemBehaviour { get; }
        #endregion
    }
}
