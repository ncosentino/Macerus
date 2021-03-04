using System.Collections.Generic;

using Autofac;

using Macerus.Plugins.Features.GameObjects.Actors.Api;

using ProjectXyz.Api.Framework;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Stats;

namespace Macerus.Plugins.Features.GameObjects.Actors.Autofac
{
    public sealed class ActorsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<ActorBehaviorsProvider>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ActorBehaviorsInterceptor>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ActorMovementSystem>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ActorRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ActorIdentifiers>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<DynamicAnimationIdentifiers>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<DynamicAnimationBehaviorFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder
                .Register(c =>
                {
                    var dynamicAnimationIdentifiers = c.Resolve<IDynamicAnimationIdentifiers>();
                    var mapping = new Dictionary<IIdentifier, string>();
                    foreach (var animationStatId in DynamicAnimationIdentifiers.GetAllStatDefinitionIds(dynamicAnimationIdentifiers))
                    {
                        mapping.Add(
                            animationStatId,
                            $"{animationStatId.ToString().ToUpperInvariant()}");
                    }

                    return new InMemoryStatDefinitionToTermMappingRepository(mapping);
                })
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}