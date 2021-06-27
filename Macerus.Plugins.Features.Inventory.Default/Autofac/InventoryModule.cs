using Autofac;

using Macerus.Plugins.Features.GameObjects.Containers.Api.LootDrops;
using Macerus.Plugins.Features.Inventory.Api;

using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Mapping;

namespace Macerus.Plugins.Features.Inventory.Default.Autofac
{
    public sealed class InventoryModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            const string ITEM_SET_NAME_PLAYER_EQUIPMENT = "player equipment";
            const string ITEM_SET_NAME_PLAYER_BAG = "player bag";
            const string ITEM_SET_NAME_DROP_TO_MAP = "drop to map";
            const string CONVERTER_NAME_EQUIPMENT = "equipment";
            const string CONVERTER_NAME_BAG = "bag";

            builder
                .RegisterType<ItemSetController>()
                .AsImplementedInterfaces()
                .SingleInstance()
                .OnActivated(x =>
                {
                    var dropToMapBinder = new ItemSetToViewModelBinder(
                        x.Context.ResolveNamed<IItemToItemSlotViewModelConverter>(CONVERTER_NAME_BAG),
                        new DropToMapItemSet(
                            x.Context.Resolve<ILootDropFactory>(),
                            x.Context.Resolve<ILootDropIdentifiers>(),
                            x.Context.Resolve<IMapGameObjectManager>(),
                            x.Context.Resolve<IMapProvider>()),
                        x.Context.ResolveNamed<IItemSlotCollectionViewModel>(ITEM_SET_NAME_DROP_TO_MAP));
                    x.Instance.Register(dropToMapBinder);
                });
            builder
                .RegisterType<BagItemSetFactory>()
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
                .RegisterType<ItemSlotCollectionViewModel>()
                .Named<IItemSlotCollectionViewModel>(ITEM_SET_NAME_DROP_TO_MAP)
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
            builder
                .RegisterType<InventorySocketingWorkflow>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}