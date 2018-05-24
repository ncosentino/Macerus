using Autofac;
using Macerus.Api.GameObjects;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Actors
{
    public sealed class ActorsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<AdditionalActorBehaviorsProvider>()
                .AsImplementedInterfaces()
                .SingleInstance();

            var actorTypeId = new StringIdentifier("actor");
            builder
                .RegisterType<ActorRepository>()
                .AsImplementedInterfaces()
                .AsSelf()
                .AutoActivate()
                .OnActivated(x =>
                {
                    var registrar = x
                        .Context
                        .Resolve<IGameObjectRepositoryRegistrar>();
                    registrar.RegisterRepository(
                        (typeId, objectId) => typeId.Equals(actorTypeId) && objectId is StringIdentifier,
                        objectId => x.Instance.Load(objectId));
                });
        }
    }
}