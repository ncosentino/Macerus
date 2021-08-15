using System.Collections.Generic;
using System.Linq;

using Autofac;

using Macerus.Plugins.Features.Encounters.Default.EndHandlers;
using Macerus.Plugins.Features.Encounters.Default.StartHandlers;

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
                .RegisterType<CombatGenerateRewardsEndEncounterHandler>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<DistributeCombatRewardsEndEncounterHandler>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<DisplayResultsEndEncounterHandler>()
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
        }
    }
}