using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public static class ComponentExtensionMethods
    {
        #region Methods
        public static TComponent GetRequiredComponent<TComponent>(this Component component)
        {
            var childComponent = (object)component.GetComponent(typeof(TComponent));
            if (childComponent == null)
            {
                throw new InvalidOperationException(string.Format("Could not get component of type '{0}' from component '{1}'.", typeof(TComponent), component));
            }

            return (TComponent)childComponent;
        }

        public static TComponent GetRequiredComponentInParent<TComponent>(this Component component)
        {
            var childComponent = (object)component.GetComponentInParent(typeof(TComponent));
            if (childComponent == null)
            {
                throw new InvalidOperationException(string.Format("Could not get component of type '{0}' from parents of component '{1}'.", typeof(TComponent), component));
            }

            return (TComponent)childComponent;
        }
        #endregion
    }
}
