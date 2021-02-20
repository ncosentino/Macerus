using System;

using Autofac;

using Macerus.Plugins.Content.Wip.Items;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Framework;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.InMemory;
using ProjectXyz.Shared.Behaviors.Filtering.Attributes;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Unique.Autofac
{
    public sealed class UniqueItemModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c =>
                {
                    var itemDefinitions = new[]
                    {
                        new ItemDefinition(
                            new[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("affix-type"),
                                    new StringFilterAttributeValue("unique"),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("item-id"),
                                    new IdentifierFilterAttributeValue(new StringIdentifier("tfos")),
                                    false)
                            },
                            new IFilterComponent[]
                            {
                                new UniqueBaseItemFilterComponent(new StringIdentifier("cloth-armor")),
                                new NameFilterComponent("Torn Flesh of Souls"),
                                new IconFilterComponent(@"graphics\items\body\cloth_armor"),
                                new EquippableFilterComponent(new[] { new StringIdentifier("body") }),
                                new SocketFilterComponent(new[]
                                {
                                    KeyValuePair.Create((IIdentifier)new StringIdentifier("gem"), Tuple.Create(0, 6)),
                                })
                            }),
                    };
                    var itemDefinitionRepository = new InMemoryItemDefinitionRepository(
                        c.Resolve<IAttributeFilterer>(),
                        itemDefinitions);
                    return itemDefinitionRepository;
                })
                .SingleInstance()
                .AsImplementedInterfaces();
            builder
                .RegisterType<UniqueItemGenerator>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<UniqueBaseItemFilterComponentToBehaviorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
