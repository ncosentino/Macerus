namespace Macerus.Plugins.Features.Inventory.Api
{
    public interface IItemSetController
    {
        void Register(IItemSetToViewModelBinder binder);

        void Unregister(IItemSetToViewModelBinder binder);

        void EndPendingDragDrop();
    }
}