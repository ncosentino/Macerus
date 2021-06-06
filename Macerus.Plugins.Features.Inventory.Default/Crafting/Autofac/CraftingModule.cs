using Autofac;

using Macerus.Plugins.Features.Inventory.Api;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.Inventory.Default.Crafting.Autofac
{
    public sealed class CraftingModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            const string ITEM_SET_NAME_PLAYER_CRAFTING_BAG = "player crafting bag";
            const string CONVERTER_NAME_BAG = "bag";

            builder
                .RegisterType<ItemSlotCollectionViewModel>()
                .Named<IItemSlotCollectionViewModel>(ITEM_SET_NAME_PLAYER_CRAFTING_BAG)
                .SingleInstance();
            builder.RegisterType<CraftingController>();
            builder
                .Register(x =>
                {
                    var factory = x.Resolve<CraftingController.Factory>();
                    var controller = factory(
                        x.ResolveNamed<IItemSlotCollectionViewModel>(ITEM_SET_NAME_PLAYER_CRAFTING_BAG),
                        x.ResolveNamed<IItemToItemSlotViewModelConverter>(CONVERTER_NAME_BAG));
                    return controller;
                })
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<CraftingWindowViewModel>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}