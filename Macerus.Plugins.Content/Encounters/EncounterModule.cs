﻿using Autofac;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Plugins.Features.Encounters;
using Macerus.Plugins.Features.Encounters.SpawnTables.Api;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.GameObjects.Generation.Default;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Content.Encounters
{
    public sealed class EncounterModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c =>
                {
                    var filterContextAmenity = c.Resolve<IFilterContextAmenity>();
                    var spawnTableIdentifiers = c.Resolve<ISpawnTableIdentifiers>();
                    var encounterDefinitions = new IEncounterDefinition[]
                    {
                        new EncounterDefinition(
                            new StringIdentifier("test-encounter"),
                            new IFilterAttribute[]
                            {
                            },
                            new IGeneratorComponent[]
                            {
                                new StatelessBehaviorGeneratorComponent(
                                    new EncounterMapFilterBehavior(new IFilterAttribute[]
                                    {
                                        // FIXME: put a map here
                                    }),
                                    new EncounterSpawnFilterBehavior(new IFilterAttribute[]
                                    {
                                        filterContextAmenity.CreateRequiredAttribute(
                                            spawnTableIdentifiers.FilterContextSpawnTableIdentifier,
                                            new StringIdentifier("test-multi-skeleton"))
                                    })),
                            }),
                    };
                    var encounterDefinitionRepository = new InMemoryEncounterDefinitionRepository(
                        encounterDefinitions);
                    return encounterDefinitionRepository;
                })
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}