using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Components;
using ProjectXyz.Application.Interface.Items;
using UnityEngine;

namespace Assets.Scripts.Gui.Inventory
{
    public abstract class DoubleClickUseItemBehaviour : MonoBehaviour
    {
        #region Fields
        private ICanRemoveItemBehaviour _canRemoveItemBehaviour;
        private IHasItemBehaviour _hasItemBehaviour;
        private IDoubleClickBehaviour _doubleClickBehaviour;
        #endregion

        #region Unity Properties
        #endregion

        #region Properties
        protected abstract ICanUseItem Target { get; }
        #endregion

        #region Methods
        public virtual void Start()
        {
            _canRemoveItemBehaviour = this.GetRequiredComponent<ICanRemoveItemBehaviour>();
            _hasItemBehaviour = this.GetRequiredComponent<IHasItemBehaviour>();
            _doubleClickBehaviour = this.GetRequiredComponent<IDoubleClickBehaviour>();
            _doubleClickBehaviour.DoubleClick += DoubleClickBehaviour_DoubleClick;
        }

        private void OnDestroy()
        {
            if (_doubleClickBehaviour != null)
            {
                _doubleClickBehaviour.DoubleClick -= DoubleClickBehaviour_DoubleClick;
            }
        }
        #endregion

        #region Event Handlers
        private void DoubleClickBehaviour_DoubleClick(object sender, EventArgs e)
        {
            if (Target == null)
            {
                throw new InvalidOperationException("The target for using items is null.");
            }
            
            var item = _hasItemBehaviour.Item;
            if (item == null ||
                !_canRemoveItemBehaviour.CanRemoveItem() ||
                !Target.CanUseItem(item))
            {
                return;
            }

            _canRemoveItemBehaviour.RemoveItem();
            Target.UseItem(item);
        }
        #endregion
    }
}
