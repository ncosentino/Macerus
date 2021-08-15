using System.Collections.Generic;
using System.Linq;

using Autofac;

using Macerus.Plugins.Features.Spawning.Default;
using Macerus.Plugins.Features.Spawning.Default.Actors;
using Macerus.Plugins.Features.Spawning.Default.Linked;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.Spawning.Default.Autofac
{
    public sealed class SpawningModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<ActorSpawnerAmenity>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ActorSpawnTableFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<LinkedSpawnTableFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<SpawnTableRepositoryFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<NoneSpawnTableRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                 .RegisterType<ActorSpawner>()
                 .AsImplementedInterfaces()
                 .SingleInstance();
            builder
                .RegisterType<ActorSpawnTableHandlerGenerator>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<LinkedSpawnTableHandlerGenerator>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<SpawnTableHandlerGeneratorFacade>()
                .AsImplementedInterfaces()
                .SingleInstance()
                .OnActivated(x =>
                {
                    // NOTE: currently this needs to be OnActivated and not
                    // done with autofac discovery because of a circular
                    // dependency w/ classes that want ISpawnTableHandlerGeneratorFacade
                    x
                     .Context
                     .Resolve<IEnumerable<IDiscoverableSpawnTableHandlerGenerator>>()
                     .Foreach(d => x.Instance.Register(d.SpawnTableType, d));
                });
            builder
                .RegisterType<SpawnTableIdentifiers>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}