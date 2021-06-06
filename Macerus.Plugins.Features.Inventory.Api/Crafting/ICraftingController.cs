namespace Macerus.Plugins.Features.Inventory.Api.Crafting
{
    public interface ICraftingController
    {
        void CloseCraftingWindow();

        void OpenCraftingWindow();

        bool ToggleCraftingWindow();
    }
}