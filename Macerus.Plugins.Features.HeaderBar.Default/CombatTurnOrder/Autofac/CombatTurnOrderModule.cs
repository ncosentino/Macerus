using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.HeaderBar.Default.CombatTurnOrder.Autofac
{
    public sealed class CombatTurnOrderModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<GameObjectToCombatTurnOrderPortraitConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<CombatTurnOrderViewModel>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<CombatTurnOrderController>()
                .AsSelf()
                .AutoActivate();
        }
    }
}