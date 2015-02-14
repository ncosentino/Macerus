using Assets.Scripts.Components;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Gui
{
    public class DraggableWindowBehaviour : MonoBehaviour, IPointerDownHandler, IDragHandler
    {
        #region Fields
        private Vector2 _pointerOffset;
        private RectTransform _canvasRectTransform;
        #endregion

        #region Unity Properties
        public RectTransform PanelRectTransform;
        #endregion

        #region Methods
        public void Awake()
        {
            _canvasRectTransform = this.GetRequiredComponent<RectTransform>();

            if (PanelRectTransform == null)
            {
                PanelRectTransform = gameObject.transform as RectTransform;
            }
        }

        public void OnPointerDown(PointerEventData data)
        {
            PanelRectTransform.SetAsLastSibling();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                PanelRectTransform,
                data.position,
                data.pressEventCamera,
                out _pointerOffset);
        }

        public void OnDrag(PointerEventData data)
        {
            if (PanelRectTransform == null)
            {
                return;
            }

            Vector2 pointerPostion = ClampToWindow(data);

            Vector2 localPointerPosition;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvasRectTransform,
                pointerPostion,
                data.pressEventCamera,
                out localPointerPosition
            ))
            {
                PanelRectTransform.localPosition = localPointerPosition - _pointerOffset;
            }
        }

        private Vector2 ClampToWindow(PointerEventData data)
        {
            var rawPointerPosition = data.position;

            var canvasCorners = new Vector3[4];
            _canvasRectTransform.GetWorldCorners(canvasCorners);

            float clampedX = Mathf.Clamp(rawPointerPosition.x, canvasCorners[0].x, canvasCorners[2].x);
            float clampedY = Mathf.Clamp(rawPointerPosition.y, canvasCorners[0].y, canvasCorners[2].y);

            Vector2 newPointerPosition = new Vector2(clampedX, clampedY);
            return newPointerPosition;
        }
        #endregion
    }
}