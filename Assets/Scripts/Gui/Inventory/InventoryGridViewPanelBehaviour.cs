using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using ProjectXyz.Application.Interface.Items;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Gui.Inventory
{
    public class InventoryGridViewPanelBehaviour : MonoBehaviour
    {
        #region Fields
        private IInventory _inventory;
        #endregion

        #region Unity Properties
        public ScrollRect ItemCollectionScrollRect;

        public GameObject InventorySlotPrefab;
        #endregion

        #region Properties
        public IInventory Inventory
        {
            get
            {
                return _inventory;
            }

            set
            {
                UnhookInventoryEvents(_inventory);
                _inventory = value;
                HookInventoryEvents(_inventory);
            }
        }
        #endregion

        #region Methods
        public void OnDestroy()
        {
            Inventory = null;
        }

        public void Start()
        {
            var mutator = ProjectXyz.Application.Core.Items.Inventory.Create();
            Inventory = mutator;
            mutator.ItemCapacity = 10;
        }

        private void HookInventoryEvents(IInventory inventory)
        {
            if (inventory == null)
            {
                return;
            }

            inventory.CapacityChanged += Inventory_CapacityChanged;
            inventory.CollectionChanged += Inventory_CollectionChanged;
        }

        private void UnhookInventoryEvents(IInventory inventory)
        {
            if (inventory == null)
            {
                return;
            }

            inventory.CapacityChanged -= Inventory_CapacityChanged;
            inventory.CollectionChanged -= Inventory_CollectionChanged;
        }

        private void PopulateItemSlots(IMutableInventory inventory, ScrollRect itemCollectionScrollRect)
        {
            (from
                 Transform child 
             in 
                 itemCollectionScrollRect.transform
             select 
                child.gameObject)
            .ToList()
            .ForEach(Destroy);
            
            if (inventory == null)
            {
                return;
            }

            const int ROW_COUNT = 10;
            const int COLUMN_COUNT = 10;

            float slotWidth = itemCollectionScrollRect.content.rect.width / ROW_COUNT;
            float slotHeight = itemCollectionScrollRect.content.rect.height / COLUMN_COUNT;

            for (int x = 0; x < COLUMN_COUNT; x++)
            {
                for (int y = 0; y < ROW_COUNT; y++)
                {
                    var slot = (GameObject)PrefabUtility.InstantiatePrefab(InventorySlotPrefab);
                    slot.transform.SetParent(itemCollectionScrollRect.content.transform);

                    var slotRectTransform = slot.GetComponent<RectTransform>();
                    slotRectTransform.SetInsetAndSizeFromParentEdge(
                        RectTransform.Edge.Left, 
                        x * slotWidth, 
                        0);
                    slotRectTransform.SetInsetAndSizeFromParentEdge(
                        RectTransform.Edge.Top,
                        y * slotHeight,
                        0);
                    slotRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotWidth);
                    slotRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotHeight);

                    var inventorySlotBehaviour = (InventorySlotBehaviourBehaviour)slot.GetComponent(typeof(InventorySlotBehaviourBehaviour));
                    inventorySlotBehaviour.Inventory = inventory;
                    inventorySlotBehaviour.InventoryIndex = x * COLUMN_COUNT + y;
                }
            }
        }
        #endregion

        #region Event Handlers
        private void Inventory_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            PopulateItemSlots((IMutableInventory)sender, ItemCollectionScrollRect);
        }

        private void Inventory_CapacityChanged(object sender, EventArgs e)
        {
            PopulateItemSlots((IMutableInventory)sender, ItemCollectionScrollRect);
        }
        #endregion
    }
}