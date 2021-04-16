using Autofac;

using Macerus.Plugins.Features.Inventory.Api;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.Inventory.Default.Autofac
{
    public sealed class InventoryModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            const string ITEM_SET_NAME_PLAYER_EQUIPMENT = "player equipment";
            const string ITEM_SET_NAME_PLAYER_BAG = "player bag";
            const string CONVERTER_NAME_EQUIPMENT = "equipment";
            const string CONVERTER_NAME_BAG = "bag";

            builder
                .RegisterType<ItemSetController>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ItemSlotCollectionViewModel>()
                .Named<IItemSlotCollectionViewModel>(ITEM_SET_NAME_PLAYER_EQUIPMENT)
                .SingleInstance();
            builder
                .RegisterType<ItemSlotCollectionViewModel>()
                .Named<IItemSlotCollectionViewModel>(ITEM_SET_NAME_PLAYER_BAG)
                .SingleInstance();
            builder
                .RegisterType<EquipmentItemToItemSlotViewModelConverter>()
                .Named<IItemToItemSlotViewModelConverter>(CONVERTER_NAME_EQUIPMENT)
                .SingleInstance();
            builder
                .RegisterType<BagItemToItemSlotViewModelConverter>()
                .Named<IItemToItemSlotViewModelConverter>(CONVERTER_NAME_BAG)
                .SingleInstance();
            builder.RegisterType<PlayerInventoryController>();
            builder
                .Register(x =>
                {
                    var factory = x.Resolve<PlayerInventoryController.Factory>();
                    var controller = factory(
                        x.ResolveNamed<IItemSlotCollectionViewModel>(ITEM_SET_NAME_PLAYER_EQUIPMENT),
                        x.ResolveNamed<IItemSlotCollectionViewModel>(ITEM_SET_NAME_PLAYER_BAG),
                        x.ResolveNamed<IItemToItemSlotViewModelConverter>(CONVERTER_NAME_EQUIPMENT),
                        x.ResolveNamed<IItemToItemSlotViewModelConverter>(CONVERTER_NAME_BAG));
                    return controller;
                })
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<PlayerInventoryViewModel>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ItemDragViewModel>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}