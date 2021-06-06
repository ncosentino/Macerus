using System;

using Macerus.Plugins.Features.Gui.Api;

namespace Macerus.Plugins.Features.Inventory.Api.Crafting
{
    public interface ICraftingWindowViewModel : IWindowViewModel
    {
        event EventHandler<EventArgs> RequestCraft;

        void Craft();
    }
}
