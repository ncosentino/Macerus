using Assets.Scripts.Components;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Gui
{
    public class DraggableWindowBehaviour : MonoBehaviour, IDragHandler
    {
        #region Unity Properties
        public RectTransform PanelRectTransform;
        #endregion

        #region Methods
        public void Start()
        {
            if (PanelRectTransform == null)
            {
                PanelRectTransform = gameObject.GetRequiredComponent<RectTransform>();
            }
        }

        public void OnDrag(PointerEventData data)
        {
            PanelRectTransform.localPosition += new Vector3(data.delta.x, data.delta.y);
        }
        #endregion
    }
}