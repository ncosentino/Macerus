using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Components;
using Assets.Scripts.GameObjects;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Gui.Inventory
{
    public class CanShowItemDetailsBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        #region Fields
        private IHasItemBehaviour _hasItemBehaviour;
        private GameObject _itemDetailsGameObject;
        private RectTransform _rectTransform;
        #endregion

        #region Unity Properties
        public GameObject ItemDetailsPrefab;

        public Image IconImage;
        #endregion

        #region Methods
        public void Start()
        {
            _hasItemBehaviour = this.GetRequiredComponent<IHasItemBehaviour>();
            _rectTransform = this.GetRequiredComponent<RectTransform>();

            if (IconImage == null)
            {
                IconImage = gameObject.GetRequiredComponent<Image>();
            }

            if (ItemDetailsPrefab == null)
            {
                throw new InvalidOperationException("The item details prefab must be set.");
            }

            if (!ItemDetailsPrefab.HasRequiredComponent<IItemDetailsBehaviour>())
            {
                throw new InvalidOperationException(string.Format("The item details prefab must have the '{0}' component.", typeof(IItemDetailsBehaviour)));
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_itemDetailsGameObject != null ||
                _hasItemBehaviour.Item == null)
            {
                return;
            }
            
            _itemDetailsGameObject = (GameObject)PrefabUtility.InstantiatePrefab(ItemDetailsPrefab);
            _itemDetailsGameObject.transform.SetParent(this.GetRequiredComponentInParent<Canvas>().transform);
            _itemDetailsGameObject.transform.position = _rectTransform.position;
            _itemDetailsGameObject.transform.Translate(_rectTransform.rect.width, _rectTransform.rect.height, 0);

            var itemDetailsBehaviour = _itemDetailsGameObject.GetRequiredComponent<IItemDetailsBehaviour>();
            itemDetailsBehaviour.Icon = IconImage.sprite;
            itemDetailsBehaviour.Item = _hasItemBehaviour.Item;

            Debug.Log(string.Format("Showing details for '{0}'.", _hasItemBehaviour.Item));
        }

        public void OnPointerExit(PointerEventData eventData)
        {
           DestroyDetails();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            DestroyDetails();
        }

        private void DestroyDetails()
        {
            if (_itemDetailsGameObject != null)
            {
                Destroy(_itemDetailsGameObject);
                _itemDetailsGameObject = null;
            }
        }

        private void OnDestroy()
        {
            DestroyDetails();
        }
        #endregion
    }
}
