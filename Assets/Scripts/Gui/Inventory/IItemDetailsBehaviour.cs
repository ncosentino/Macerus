using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Items;
using UnityEngine;

namespace Assets.Scripts.Gui.Inventory
{
    public interface IItemDetailsBehaviour
    {
        #region Properties
        IItem Item { get; set; }

        Sprite Icon { get; set; }
        #endregion
    }
}
