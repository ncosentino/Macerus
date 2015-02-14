using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Items.ExtensionMethods;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Gui.Inventory
{
    public class InventorySlotBehaviour : MonoBehaviour, IInventorySlotBehaviourBehaviour
    {
        #region Unity Properties
        public Image IconImage;

        public Sprite EmptySprite;
        #endregion

        #region Properties
        public int InventoryIndex { get; set; }

        public IMutableInventory Inventory { get; set; }

        public IItem Item
        {
            get { return Inventory[InventoryIndex]; }
        }
        #endregion

        #region Methods
        public void Start()
        {
            if (IconImage == null)
            {
                IconImage = GetComponent<Image>();
            }

            if (EmptySprite == null)
            {
                EmptySprite = IconImage.sprite;
            }

            // TODO: grab the resource off of the item itself.
            if (Item != null)
            {
                IconImage.sprite = Resources.Load<Sprite>("Graphics/Items/Gloves/leather gloves");
            }
        }

        public bool CanRemoveItem()
        {
            var result = Inventory.SlotOccupied(InventoryIndex);
            Debug.Log(string.Format("Can remove from {0}? {1}", InventoryIndex, result));

            return result;
        }

        public IItem RemoveItem()
        {
            var item = Item;
            Inventory.Remove(Item);
            Debug.Log(string.Format("Removed {0} from {1}.", item, InventoryIndex));

            IconImage.sprite = EmptySprite;

            return item;
        }

        public bool CanAddItem(IItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Cannot add null item.");
            }

            var result = !Inventory.SlotOccupied(InventoryIndex);
            Debug.Log(string.Format("Can {0} be added? {1}", item, result));

            return result;
        }

        public void AddItem(IItem item)
        {
            Inventory.Add(item, InventoryIndex);
            Debug.Log(string.Format("Added {0}.", item));

            // TODO: grab the resource off of the item itself.
            IconImage.sprite = Resources.Load<Sprite>("Graphics/Items/Gloves/leather gloves");
        }
        #endregion
    }
}
