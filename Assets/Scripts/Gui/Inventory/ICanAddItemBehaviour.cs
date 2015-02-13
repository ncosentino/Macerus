using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectXyz.Application.Interface.Items;

namespace Assets.Scripts.Gui.Inventory
{
    public interface ICanAddItemBehaviour
    {
        #region Methods
        bool CanAddItem(IItem item);

        void AddItem(IItem item);
        #endregion
    }
}
