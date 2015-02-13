using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectXyz.Application.Interface.Items;

namespace Assets.Scripts.Gui.Inventory
{
    public interface ICanRemoveItemBehaviour
    {
        #region Methods
        bool CanRemoveItem();

        IItem RemoveItem();
        #endregion
    }
}
