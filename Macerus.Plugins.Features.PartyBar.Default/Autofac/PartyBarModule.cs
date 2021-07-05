using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.PartyBar.Default.Autofac
{
    public sealed class PartyBarModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<GameObjectToPartyBarPortraitConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<PartyBarViewModel>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<PartyBarController>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}