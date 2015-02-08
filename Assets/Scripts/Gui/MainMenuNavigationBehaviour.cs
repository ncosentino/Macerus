using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Gui
{
    public class MainMenuNavigationBehaviour : MonoBehaviour
    {
        #region Event Handlers
        public void NewGameButton_OnClick()
        {
            Debug.Log("New game clicked.");
            Application.LoadLevel("Explore");
        }

        public void ExitButton_OnClick()
        {
            Debug.Log("Exit clicked.");
            Application.Quit();
        }
        #endregion
    }
}