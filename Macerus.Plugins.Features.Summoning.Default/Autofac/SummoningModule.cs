using Autofac;

using Macerus.Plugins.Features.Summoning;
using Macerus.Plugins.Features.Summoning.Default;

using ProjectXyz.Framework.Autofac;

namespace Macerus.Plugins.Features.Spawning.Default.Autofac
{
    public sealed class SummoningModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<SummoningBehaviorFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<SummonHandlerFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<SpawnSummonHandler>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<SpawnLimitSummonHandler>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<TeamAssignmentSummonHandler>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
               .RegisterType<SummonLimitingSystem>()
               .AsImplementedInterfaces()
               .SingleInstance();
            builder
               .RegisterType<SummonMapPlacementSystem>()
               .AsImplementedInterfaces()
               .SingleInstance();
            builder
               .RegisterType<SummonLimitStatPairRepositoryFacade>()
               .AsImplementedInterfaces()
               .SingleInstance();
            builder
               .RegisterType<NoneSummonHandlerLoadOrder>()
               .AsImplementedInterfaces()
               .IfNotRegistered(typeof(ISummonHandlerLoadOrder))
               .SingleInstance();
        }
    }
}