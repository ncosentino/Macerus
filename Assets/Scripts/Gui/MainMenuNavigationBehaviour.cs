using UnityEngine;

namespace Assets.Scripts.Gui
{
    public sealed class MainMenuNavigationBehaviour : MonoBehaviour
    {
        #region Event Handlers
        public void NewGameButton_OnClick()
        {
            Debug.Log("New character clicked.");
            Application.LoadLevel(1);
        }

        public void ExitButton_OnClick()
        {
            Debug.Log("Exit clicked.");
            Application.Quit();
        }
        #endregion
    }
}