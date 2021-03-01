using System.Linq;

using Autofac;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes; // FIXME: dependency on non-API
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables.Standard;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables.Implementations.Item;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.InMemory.DropTables;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.DropTables
{
    public sealed class DropTableModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(x =>
                {
                    var dropTableFactory = x.Resolve<IItemDropTableFactory>();
                    var dropTables = new IDropTable[]
                    {
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
                            new StringIdentifier("any_normal_magic_10x_lvl10"),
                            10,
                            10,
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
                    };
                    return new InMemoryDropTableRepository(dropTables);
                })
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<DropTablIdentifiers>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
