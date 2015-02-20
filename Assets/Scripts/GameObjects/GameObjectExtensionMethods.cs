using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.GameObjects
{
    public static class GameObjectExtensionMethods
    {
        #region Methods
        public static TComponent GetRequiredComponent<TComponent>(this GameObject gameObject)
        {
            Contract.Requires(gameObject != null);
            Contract.Ensures(Contract.Result<TComponent>() != null);

            var childComponent = (object)gameObject.GetComponent(typeof(TComponent));
            if (childComponent == null)
            {
                throw new InvalidOperationException(string.Format("Could not get component of type '{0}' from game object '{1}'.", typeof(TComponent), gameObject));
            }

            return (TComponent)childComponent;
        }

        public static bool HasRequiredComponent<TComponent>(this GameObject gameObject)
        {
            Contract.Requires(gameObject != null);

            var childComponent = (object)gameObject.GetComponent(typeof(TComponent));
            return childComponent != null;
        }

        public static TComponent GetRequiredComponentInParent<TComponent>(this GameObject gameObject)
        {
            Contract.Requires(gameObject != null);
            Contract.Ensures(Contract.Result<TComponent>() != null);

            var childComponent = (object)gameObject.GetComponentInParent(typeof(TComponent));
            if (childComponent == null)
            {
                throw new InvalidOperationException(string.Format("Could not get component of type '{0}' from parents of game object '{1}'.", typeof(TComponent), gameObject));
            }

            return (TComponent)childComponent;
        }
        #endregion
    }
}
