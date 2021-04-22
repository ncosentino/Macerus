using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.GameObjects.Actors.Generation.Autofac
{
    public sealed class ActorsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<ActorDefinitionRepositoryFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<NormalActorGenerator>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ActorGeneratorFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<SpawnWithItemsGeneratorComponentToBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}