using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Items;

namespace Assets.Scripts.Gui.Inventory
{
    public interface IInventorySlotBehaviour : ICanAddItemBehaviour, ICanRemoveItemBehaviour, IHasItemBehaviour
    {
        #region Properties
        int InventoryIndex { get; set; }

        IMutableInventory Inventory { get; set; }
        #endregion
    }
}
