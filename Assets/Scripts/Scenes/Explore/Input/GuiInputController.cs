using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Scenes.Explore.Gui;

namespace Assets.Scripts.Scenes.Explore.Input
{
    public class GuiInputController : IGuiInputController
    {
        #region Fields
        private readonly IHudController _hudController;
        private readonly IKeyboardControls _keyboardControls;
        #endregion

        #region Constructors
        public GuiInputController(IKeyboardControls keyboardControls, IHudController hudController)
        {
            _keyboardControls = keyboardControls;
            _hudController = hudController;
        }
        #endregion

        #region Methods
        public void Update(float deltaTime)
        {
            if (UnityEngine.Input.GetKeyUp(_keyboardControls.ToggleInventory))
            {
                _hudController.ShowInventory(!_hudController.InventoryVisible);
            }
        }
        #endregion
    }
}
