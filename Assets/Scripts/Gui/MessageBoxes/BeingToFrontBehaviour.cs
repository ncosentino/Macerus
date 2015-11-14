using UnityEngine;

namespace Assets.Scripts.Gui.MessageBoxes
{
    public sealed class BringToFrontBehaviour : MonoBehaviour
    {
        #region Methods
        public void OnEnable()
        {
            transform.SetAsLastSibling();
        }
        #endregion
    }
}
