using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.Minimap.Default.Autofac
{
    public sealed class MinimapModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<MinimapController>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MinimapOverlayViewModel>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MinimapBadgeViewModel>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}