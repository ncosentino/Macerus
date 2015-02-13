using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Gui.Inventory
{
    public class InventoryDraggedItemBehaviour : MonoBehaviour, IInventoryDraggedItemBehaviour
    {
        #region Properties
        public IInventoryDragItemSourceBehaviour Source
        {
            get; set;
        }

        public IHasItemBehaviour HasItemBehaviour
        {
            get;
            set;
        }
        #endregion

        #region Methods
        public void OnDrag(PointerEventData eventData)
        {
            eventData.pointerDrag.transform.Translate(eventData.delta);
        }
        
        public void OnEndDrag(PointerEventData eventData)
        {
            Destroy(eventData.pointerDrag);
        }
        #endregion
    }
}