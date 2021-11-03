using System.Linq;

using Autofac;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Plugins.Features.Encounters;

using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes; // FIXME: dependency on non-API
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables.Implementations.Linked;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables.Linked;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables.Standard;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.InMemory.DropTables;
using ProjectXyz.Shared.Framework;

namespace Macerus.Content.Generated.Items
{
    public sealed class DropTablesModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c =>
                {
                    var dropTableFactory = c.Resolve<IItemDropTableFactory>();
                    var linkedDropTableFactory = c.Resolve<ILinkedDropTableFactory>();
                    var encounterIdentifiers = c.Resolve<IEncounterIdentifiers>();
                    var filterContextAmenity = c.Resolve<IFilterContextAmenity>();
                    var dropTables = new IDropTable[]
                    {
                        linkedDropTableFactory.Create(
                            new StringIdentifier("apprentice_3-5@1_normal/magic/rare"),
                            3,
                            3,
                            new IWeightedEntry[]
                            {
                             new WeightedEntry(60, new StringIdentifier("_apprentice_1@1_normal")),
                             new WeightedEntry(30, new StringIdentifier("_apprentice_1@1_magic")),
                             new WeightedEntry(10, new StringIdentifier("_apprentice_1@1_rare"))
                            },
                            new IFilterAttribute[]
                            {
                                // required item level was 0 so ignoring
                            },
                            new IFilterAttribute[]
                            {
                                
                                
                            }),
                        dropTableFactory.Create(
                            new StringIdentifier("_apprentice_1@1_normal"),
                            1,
                            1,
                            new IFilterAttribute[]
                            {
                                // required item level was 0 so ignoring
                            },
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("affix-type"),
                                    new AnyStringCollectionFilterAttributeValue("normal"),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("item-level"),
                                    new DoubleFilterAttributeValue(1),
                                    false),
                            }),
                        dropTableFactory.Create(
                            new StringIdentifier("_apprentice_1@1_magic"),
                            1,
                            1,
                            new IFilterAttribute[]
                            {
                                // required item level was 0 so ignoring
                            },
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("affix-type"),
                                    new AnyStringCollectionFilterAttributeValue("magic"),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("item-level"),
                                    new DoubleFilterAttributeValue(1),
                                    false),
                            }),
                        dropTableFactory.Create(
                            new StringIdentifier("_apprentice_1@1_rare"),
                            1,
                            1,
                            new IFilterAttribute[]
                            {
                                // required item level was 0 so ignoring
                            },
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("affix-type"),
                                    new AnyStringCollectionFilterAttributeValue("rare"),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("item-level"),
                                    new DoubleFilterAttributeValue(1),
                                    false),
                            })
                    };
                    return new InMemoryDropTableRepository(dropTables);
                })
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}