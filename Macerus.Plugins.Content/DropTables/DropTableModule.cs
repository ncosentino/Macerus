using System.Linq;

using Autofac;

using Macerus.Plugins.Features.Encounters;

using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes; // FIXME: dependency on non-API
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables.Standard;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.InMemory.DropTables;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Content.DropTables
{
    public sealed class DropTableModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(x =>
                {
                    var dropTableFactory = x.Resolve<IItemDropTableFactory>();
                    var encounterIdentifiers = x.Resolve<IEncounterIdentifiers>();
                    var dropTables = new IDropTable[]
                    {
                        dropTableFactory.Create(
                            new StringIdentifier("test-skeleton-drop"),
                            3,
                            3,
                            Enumerable.Empty<IFilterAttribute>(),
                            new[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("affix-type"),
                                    new AnyStringCollectionFilterAttributeValue("normal", "magic"),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("item-level"),
                                    new DoubleFilterAttributeValue(0),
                                    false),
                            }),
                        dropTableFactory.Create(
                            new StringIdentifier("any_magic_1-10_lvl10"),
                            1,
                            10,
                            Enumerable.Empty<IFilterAttribute>(),
                            new[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("affix-type"),
                                    new StringFilterAttributeValue("magic"),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("item-level"),
                                    new DoubleFilterAttributeValue(0),
                                    false),
                            }),
                        dropTableFactory.Create(
                            new StringIdentifier("any_normal_magic_rare_10x_lvl10"),
                            10,
                            10,
                            Enumerable.Empty<IFilterAttribute>(),
                            new[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("affix-type"),
                                    new AnyStringCollectionFilterAttributeValue("normal", "magic", "rare"),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("item-level"),
                                    new DoubleFilterAttributeValue(0),
                                    false),
                            }),
                        dropTableFactory.Create(
                            new StringIdentifier("test-encounter-win-drop"),
                            10,
                            10,
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    encounterIdentifiers.FilterEncounterCombatPlayerWonId,
                                    new BooleanFilterAttributeValue(true),
                                    true),
                            },
                            new[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("affix-type"),
                                    new AnyStringCollectionFilterAttributeValue("normal", "magic", "rare"),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("item-level"),
                                    new DoubleFilterAttributeValue(0),
                                    false),
                            }),
                    };
                    return new InMemoryDropTableRepository(dropTables);
                })
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<DropTableIdentifiers>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
