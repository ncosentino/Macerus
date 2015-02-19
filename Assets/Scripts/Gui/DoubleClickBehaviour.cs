using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Gui
{
    public class DoubleClickBehaviour : MonoBehaviour, IDoubleClickBehaviour, IPointerClickHandler
    {
        #region Events
        public event EventHandler<EventArgs> DoubleClick;
        #endregion

        #region Methods
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.clickCount != 2)
            {
                return;
            }

            OnDoubleClick();
        }
        
        private void OnDoubleClick()
        {
            Debug.Log("Double click!");
            var handler = DoubleClick;
            if (handler != null)
            {
                handler.Invoke(this, EventArgs.Empty);
            }
        }
        #endregion
    }
}
