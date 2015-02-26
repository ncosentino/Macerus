using ProjectXyz.Application.Interface.Interactions;

namespace Assets.Scripts.Interactables
{
    public interface IInteractableBehaviour
    {
        #region Properties
        IInteractable Interactable { get; set; }
        #endregion
    }
}
