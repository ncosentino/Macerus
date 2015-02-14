using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Items;

namespace Assets.Scripts.Gui.Inventory
{
    public interface IInventoryGridViewPanelBehaviour
    {
        #region Properties
        IMutableInventory Inventory { get; set; }
        #endregion
    }
}
