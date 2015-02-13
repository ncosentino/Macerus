﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Gui.Inventory
{
    public interface IEquipmentSlotBehaviourBehaviour : ICanAddItemBehaviour, ICanRemoveItemBehaviour, IHasItemBehaviour
    {
        #region Properties
        string EquipmentSlotType { get; }
        #endregion
    }
}
