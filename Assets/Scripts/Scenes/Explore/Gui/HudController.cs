using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Gui
{
    public class HudController : IHudController
    {
        #region Fields
        private readonly Transform _canvasTransform;
        private readonly Transform _inventoryWindowTransform;
        #endregion

        #region Constructors
        public HudController(Transform canvasTransform, Transform inventoryWindowTransform)
        {
            _canvasTransform = canvasTransform;
            _inventoryWindowTransform = inventoryWindowTransform;
        }
        #endregion

        #region Properties
        public bool InventoryVisible
        {
            get { return _inventoryWindowTransform.parent != null; }
        }
        #endregion

        #region Methods
        public void ShowInventory(bool show)
        {
            _inventoryWindowTransform.SetParent(show
                ? _canvasTransform
                : null);
        }
        #endregion
    }
}
