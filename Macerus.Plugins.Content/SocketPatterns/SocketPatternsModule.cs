using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Content.SocketPatterns
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