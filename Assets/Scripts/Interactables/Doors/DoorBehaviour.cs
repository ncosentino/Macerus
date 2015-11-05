using Assets.Scripts.Components;
using ProjectXyz.Application.Core.GameObjects.Doors;
using UnityEngine;

namespace Assets.Scripts.Interactables.Doors
{
    public class DoorBehaviour : MonoBehaviour
    {
        #region Fields
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private IInteractableBehaviour _interactableBehaviour;

        private IMutableDoor _door;
        #endregion

        #region Methods
        public void Start()
        {
            _spriteRenderer = this.GetRequiredComponent<SpriteRenderer>();
            _animator = this.GetRequiredComponent<Animator>();

            _door = Door.Create("", true);
            _door.OpenChanged += Door_OpenChanged;
            UpdateDoorVisualState(_door, _animator);

            _interactableBehaviour = this.GetRequiredComponentInChildren<IInteractableBehaviour>();
            _interactableBehaviour.Interactable = _door;
        }
    
        public void LateUpdate()
        {
            // little trick to allow 2D sprites to go in front and behind each other
            const int PIXEL_OFFSET = 10;
            _spriteRenderer.sortingOrder = (int)((UnityEngine.Camera.main.WorldToScreenPoint(_spriteRenderer.bounds.min).y + PIXEL_OFFSET) * -10);
        }

        private void UpdateDoorVisualState(IDoor door, Animator animator)
        {
            bool opened = door.IsOpen;
            animator.SetBool("Opened", opened);
        }
        #endregion

        #region Event Handlers
        private void Door_OpenChanged(object sender, System.EventArgs e)
        {
            var door = (IDoor)sender;
            UpdateDoorVisualState(door, _animator);

            if (door.IsOpen)
            {
                Debug.Log(_door + " is open.");
            }
            else
            {
                Debug.Log(_door + " is closed.");
            }
        }
        #endregion
    }
}
