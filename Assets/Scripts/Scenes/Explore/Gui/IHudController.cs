using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Gui
{
    public interface IHudController
    {
        #region Properties
        bool InventoryVisible { get; }
        #endregion

        #region Methods
        void ShowInventory(bool show);
        #endregion
    }
}
