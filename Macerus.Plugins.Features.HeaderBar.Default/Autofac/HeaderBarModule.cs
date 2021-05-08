using Autofac;

using Macerus.Plugins.Features.HeaderBar.Api;
using Macerus.Plugins.Features.StatusBar.Default;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.HeaderBar.Default.Autofac
{
    public sealed class HeaderBarModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<HeaderBarViewModel>()
                .As<IHeaderBarViewModel>()
                .SingleInstance();
        }
    }
}