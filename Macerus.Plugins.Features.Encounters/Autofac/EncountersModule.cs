using Autofac;

using Macerus.Plugins.Features.Encounters.GamObjects.Static.Triggers;

using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.SpawnTables;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.SpawnTables.Implementations.Item;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.SpawnTables.Implementations.Linked;

namespace Macerus.Plugins.Features.Encounters.Autofac
{
    public sealed class EncountersModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<EncounterBehaviorsProvider>()
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
                .RegisterType<ItemDefinitionRepositoryFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<NoneItemDefinitionRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<BaseItemGenerator>()
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
                .SingleInstance();
        }
    }
}