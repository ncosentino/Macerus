using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Gui.Inventory
{
    [RequireComponent(typeof(ICanRemoveItemBehaviour))]
    [RequireComponent(typeof(IHasItemBehaviour))]
    public class InventoryDragItemSourceBehaviour : MonoBehaviour, IInventoryDragItemSourceBehaviour
    {
        #region Fields
        private Canvas _canvas;
        private ICanRemoveItemBehaviour _canRemoveItemBehaviour;
        private IHasItemBehaviour _hasItemBehaviour;
        #endregion

        #region Unity Properties
        public Image IconImage;
        #endregion

        #region Properties
        public GameObject GameObject
        {
            get { return gameObject; }
        }

        public ICanRemoveItemBehaviour CanRemoveItemBehaviour
        {
            get { return _canRemoveItemBehaviour; }
        }
        #endregion

        #region Methods
        public void Start()
        {
            _canvas = gameObject.GetComponentInParent<Canvas>();
            _canRemoveItemBehaviour = (ICanRemoveItemBehaviour)gameObject.GetComponent(typeof(ICanRemoveItemBehaviour));
            _hasItemBehaviour = (IHasItemBehaviour)gameObject.GetComponent(typeof(IHasItemBehaviour));

            if (IconImage == null)
            {
                IconImage = GetComponent<Image>();
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!_canRemoveItemBehaviour.CanRemoveItem())
            {
                return;
            }

            var prefab = (GameObject)Resources.Load("Prefabs/Gui/Inventory/DraggedInventoryItem");
            var dragItem = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            dragItem.name = "Dragged Item";
            dragItem.transform.SetParent(_canvas.transform);
            dragItem.transform.position = eventData.position;
            dragItem.transform.Translate(new Vector3(64, -64));
            
            var draggedItemBehaviour = (IInventoryDraggedItemBehaviour)dragItem.GetComponent((typeof(IInventoryDraggedItemBehaviour)));
            draggedItemBehaviour.Source = this;
            draggedItemBehaviour.HasItemBehaviour = _hasItemBehaviour;
            draggedItemBehaviour.Icon = IconImage.sprite;

            eventData.pointerDrag = dragItem;
        }

        public void OnDrag(PointerEventData eventData)
        {
        }
        #endregion
    }
}