using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public static class GameObjectExtensionMethods
    {
        #region Methods
        public static TComponent GetRequiredComponent<TComponent>(this GameObject gameObject)
        {
            var childComponent = (object)gameObject.GetComponent(typeof(TComponent));
            if (childComponent == null)
            {
                throw new InvalidOperationException(string.Format("Could not get component of type '{0}' from game object '{1}'.", typeof(TComponent), gameObject));
            }

            return (TComponent)childComponent;
        }

        public static TComponent GetRequiredComponentInParent<TComponent>(this GameObject gameObject)
        {
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
