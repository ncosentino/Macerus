using System.Linq;

using Autofac;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Plugins.Features.Encounters.SpawnTables;
using Macerus.Plugins.Features.Encounters.SpawnTables.Api;
using Macerus.Plugins.Features.Encounters.SpawnTables.Api.Standard;

using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Content.SpawnTables
{
    public sealed class SpawnTableModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(x =>
                {
                    var filterContextAmenity = x.Resolve<IFilterContextAmenity>();
                    var gameObjectIdentifiers = x.Resolve<IGameObjectIdentifiers>();
                    var actorIdentifiers = x.Resolve<IActorIdentifiers>();
                    var actorSpawnTableFactory = x.Resolve<IActorSpawnTableFactory>();
                    var dropTables = new ISpawnTable[]
                    {
                        actorSpawnTableFactory.Create(
                            new StringIdentifier("test-multi-skeleton"),
                            2,
                            3,
                            Enumerable.Empty<IFilterAttribute>(),
                            new[]
                            {
                                filterContextAmenity.CreateRequiredAttribute(
                                    gameObjectIdentifiers.FilterContextTypeId,
                                    actorIdentifiers.ActorTypeIdentifier),
                                filterContextAmenity.CreateRequiredAttribute(
                                    actorIdentifiers.ActorDefinitionIdentifier,
                                    new StringIdentifier("test-skeleton")),
                                filterContextAmenity.CreateRequiredAttribute(
                                    new StringIdentifier("affix-type"),
                                    "normal"),
                            },
                            new IGeneratorComponent[] { }),
                    };
                    return new InMemorySpawnTableRepository(dropTables);
                })
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
