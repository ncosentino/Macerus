using System.Collections.Generic;
using System.Linq;

using Autofac;

using Macerus.Plugins.Features.Encounters.SpawnTables;
using Macerus.Plugins.Features.Encounters.SpawnTables.Api;
using Macerus.Plugins.Features.Encounters.SpawnTables.Implementations.Actors;
using Macerus.Plugins.Features.Encounters.SpawnTables.Implementations.Linked;
using Macerus.Plugins.Features.Encounters.Triggers.GamObjects.Static;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.Encounters.Autofac
{
    public sealed class EncountersModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<StartEncounterHandlerFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<EncounterSpawnStartHandler>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<EncounterCombatStartHandler>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<EncounterRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<EncounterMapLoadStartHandler>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<EncounterManager>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<EncounterGameObjectPlacer>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<EncounterDefinitionRepositoryFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<EncounterTriggerBehaviorsProvider>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<EncounterSpawnLocationBehaviorsProvider>()
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