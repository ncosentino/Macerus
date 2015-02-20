using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Gui.Inventory
{
    public class ItemDetailsBehaviour : MonoBehaviour, IItemDetailsBehaviour
    {
        #region Fields
        private IItem _item;
        #endregion

        #region Unity Properties
        public Image IconImage;

        public Text DescriptionText;
        #endregion

        #region Properties
        public IItem Item
        {
            get
            {
                return _item;
            }

            set
            {
                _item = value;
                RefreshItemDisplay(_item);
            }
        }

        public Sprite Icon
        {
            get { return IconImage.sprite; }
            set { IconImage.sprite = value; }
        }
        #endregion

        #region Methods
        public void Start()
        {
        }

        private void RefreshItemDisplay(IItem item)
        {
            DescriptionText.text = item.ToString();
        }
        #endregion
    }
}
