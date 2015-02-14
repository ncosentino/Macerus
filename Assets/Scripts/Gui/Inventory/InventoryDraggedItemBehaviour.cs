using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Components;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Gui.Inventory
{
    public class InventoryDraggedItemBehaviour : MonoBehaviour, IInventoryDraggedItemBehaviour
    {
        #region Unity Properties
        public Image IconImage;
        #endregion

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

        public Sprite Icon
        {
            get;
            set;
        }
        #endregion

        #region Methods
        public void Start()
        {
            if (IconImage == null)
            {
                IconImage = this.GetRequiredComponent<Image>();
            }

            IconImage.sprite = Icon;
        }

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