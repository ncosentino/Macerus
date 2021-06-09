using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.Hud.Default.Autofac
{
    public sealed class HudModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<HudViewModel>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<HudController>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}