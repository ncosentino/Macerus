using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.GameObjects.Actors.Default.Interactions.Autofac
{
    public sealed class InteractionModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<ActorInteractionManager>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ActorActionCheck>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}