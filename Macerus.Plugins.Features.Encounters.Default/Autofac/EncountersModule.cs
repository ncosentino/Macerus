using System.Collections.Generic;
using System.Linq;

using Autofac;

using Macerus.Plugins.Features.Encounters.Default.EndHandlers;
using Macerus.Plugins.Features.Encounters.Default.SpawnTables;
using Macerus.Plugins.Features.Encounters.Default.SpawnTables.Actors;
using Macerus.Plugins.Features.Encounters.Default.SpawnTables.Linked;
using Macerus.Plugins.Features.Encounters.Default.StartHandlers;
using Macerus.Plugins.Features.Encounters.SpawnTables;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.Encounters.Default.Autofac
{
    public sealed class EncountersModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<NoneEncounterIdentifiers>()
                .AsImplementedInterfaces()
                .IfNotRegistered(typeof(IEncounterIdentifiers))
                .SingleInstance();
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
                .RegisterType<EncounterTurnBasedStartHandler>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<NoneEncounterStartLoadOrder>()
                .AsImplementedInterfaces()
                .IfNotRegistered(typeof(IEncounterStartLoaderOrder))
                .SingleInstance();
            builder
                .RegisterType<EncounterDebugPrinterStartHandler>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<EndEncounterHandlerFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<EncounterTurnBasedEndHandler>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<CombatOutcomeEndEncounterHandler>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<NoneEncounterEndLoadOrder>()
                .AsImplementedInterfaces()
                .IfNotRegistered(typeof(IEncounterStartLoaderOrder))
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