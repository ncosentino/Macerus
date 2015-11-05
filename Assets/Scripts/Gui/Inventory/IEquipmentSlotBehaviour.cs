using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Gui.Inventory
{
    public interface IEquipmentSlotBehaviour : ICanAddItemBehaviour, ICanRemoveItemBehaviour, IHasItemBehaviour
    {
        #region Properties
        Guid EquipmentSlotType { get; }
        #endregion
    }
}
