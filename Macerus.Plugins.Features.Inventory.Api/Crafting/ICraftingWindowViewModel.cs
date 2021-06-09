using System;

using Macerus.Plugins.Features.Hud;

namespace Macerus.Plugins.Features.Inventory.Api.Crafting
{
    public interface ICraftingWindowViewModel : IDiscoverableHudWindowViewModel
    {
        event EventHandler<EventArgs> RequestCraft;

        void Craft();
    }
}
