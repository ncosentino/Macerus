using UnityEngine;

namespace Assets.Scripts.Gui
{
    public class ClosableWindowBehaviour : MonoBehaviour
    {
        #region Unity Properties
        public Transform WindowTransform;
        #endregion

        #region Methods
        public void Start()
        {
            if (WindowTransform == null)
            {
                WindowTransform = gameObject.transform;
            }
        }

        public void CloseButton_Click()
        {
            WindowTransform.SetParent(null);
        }
        #endregion
    }
}