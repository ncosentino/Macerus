using System;
using System.Collections.Generic;

using Autofac;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Plugins.Features.Combat.Api;
using Macerus.Plugins.Features.Combat.Default;
using Macerus.Plugins.Features.Encounters;
using Macerus.Plugins.Features.Encounters.Default;
using Macerus.Plugins.Features.Encounters.Default.EndHandlers;
using Macerus.Plugins.Features.Encounters.Default.StartHandlers;
using Macerus.Plugins.Features.Encounters.SpawnTables;

using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Generation.Default;
using ProjectXyz.Plugins.Features.Mapping;
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
                    var combatTeamIdentifiers = c.Resolve<ICombatTeamIdentifiers>();
                    var mapIdentifiers = c.Resolve<IMapIdentifiers>();
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
                                    new EncounterCombatBehavior(),
                                    new EncounterMapFilterBehavior(new[] 
                                    {
                                        filterContextAmenity.CreateRequiredAttribute(
                                            mapIdentifiers.FilterContextMapIdentifier,
                                            new StringIdentifier("test_encounter_map")),
                                    }),
                                    new EncounterSpawnTableIdBehavior(
                                        new StringIdentifier("test-multi-skeleton"),
                                        new[]
                                        {
                                            new CombatTeamGeneratorComponent(combatTeamIdentifiers.EnemyTeam1StatValue),
                                        })),
                            }),
                    };
                    var encounterDefinitionRepository = new InMemoryEncounterDefinitionRepository(
                        encounterDefinitions);
                    return encounterDefinitionRepository;
                })
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .Register(c => new EncounterStartLoadOrder(new Dictionary<Type, int>()
                {
                    [typeof(EncounterMapLoadStartHandler)] = 10000,
                    [typeof(EncounterSpawnStartHandler)] = 20000,
                    [typeof(EncounterTurnBasedStartHandler)] = int.MaxValue - 20000,
                    [typeof(EncounterCombatStartHandler)] = int.MaxValue - 10000,
                    [typeof(EncounterDebugPrinterStartHandler)] = int.MaxValue,
                }))
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .Register(c => new EncounterEndLoadOrder(new Dictionary<Type, int>()
                {
                    [typeof(EncounterTurnBasedEndHandler)] = 10000,
                }))
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
