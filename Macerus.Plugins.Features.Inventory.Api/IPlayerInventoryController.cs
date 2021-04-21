namespace Macerus.Plugins.Features.Inventory.Api
{
    public interface IPlayerInventoryController
    {
        void CloseInventory();

        void OpenInventory();

        bool ToggleInventory();
    }
}