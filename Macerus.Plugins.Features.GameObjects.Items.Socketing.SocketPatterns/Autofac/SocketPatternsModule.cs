using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.GameObjects.Items.Socketing.SocketPatterns.Autofac
{
    public sealed class SocketPatternsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<SocketPatternHandler>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}