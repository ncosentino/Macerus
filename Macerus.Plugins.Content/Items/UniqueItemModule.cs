using System;

using Autofac;

using Macerus.Plugins.Content.Wip.Items;
using Macerus.Plugins.Features.GameObjects.Items;
using Macerus.Plugins.Features.GameObjects.Items.Behaviors;
using Macerus.Plugins.Features.GameObjects.Items.Socketing;

using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes; // FIXME: dependency on non-API
using ProjectXyz.Plugins.Features.GameObjects.Generation.Default;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.InMemory;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Content.Items
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
                            new IGeneratorComponent[]
                            {
                                new UniqueBaseItemGeneratorComponent(new StringIdentifier("cloth-armor")),
                                new NameGeneratorComponent("Torn Flesh of Souls"),
                                new IconGeneratorComponent(@"graphics\items\body\cloth_armor"),
                                new EquippableGeneratorComponent(new[] { new StringIdentifier("body") }),
                                new SocketGeneratorComponent(new[]
                                {
                                    KeyValuePair.Create((IIdentifier)new StringIdentifier("gem"), Tuple.Create(0, 6)),
                                }),
                                new StatelessBehaviorGeneratorComponent(new[]
                                {
                                    new HasInventoryIconColor(0.5f, 0x80, 0, 0, 0x40),
                                }),
                            }),
                    };
                    var itemDefinitionRepository = new InMemoryItemDefinitionRepository(
                        c.Resolve<IAttributeFilterer>(),
                        itemDefinitions);
                    return itemDefinitionRepository;
                })
                .SingleInstance()
                .AsImplementedInterfaces();
        }
    }
}
