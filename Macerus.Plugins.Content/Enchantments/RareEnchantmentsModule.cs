using Autofac;

using Macerus.Plugins.Features.GameObjects.Items.Generation.Rare.Enchantments;

using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Enchantments.Generation.InMemory;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Content.Enchantments
{
    public sealed class RareEnchantmentsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c =>
                {
                    var enchantmentTemplate = new EnchantmentTemplate(c.Resolve<ICalculationPriorityFactory>());
                    var enchantmentDefinitions = new[]
                    {
                        enchantmentTemplate.CreateRareRangeEnchantment(
                            new IntIdentifier(1), // max life
                            1,
                            15,
                            0,
                            20),
                        enchantmentTemplate.CreateRareRangeEnchantment(
                            new IntIdentifier(3), // max mana
                            1,
                            15,
                            0,
                            10),
                        enchantmentTemplate.CreateRareRangeEnchantment(
                            new StringIdentifier("fireresist"), // FIXME: just for more sample enchantments
                            1,
                            15,
                            0,
                            10),
                        enchantmentTemplate.CreateRareRangeEnchantment(
                            new StringIdentifier("iceresist"), // FIXME: just for more sample enchantments
                            1,
                            15,
                            0,
                            10),
                        enchantmentTemplate.CreateRareRangeEnchantment(
                            new StringIdentifier("waterresist"), // FIXME: just for more sample enchantments
                            1,
                            15,
                            0,
                            10),
                        enchantmentTemplate.CreateRareRangeEnchantment(
                            new StringIdentifier("lightningresist"), // FIXME: just for more sample enchantments
                            1,
                            15,
                            0,
                            10),
                        enchantmentTemplate.CreateRareRangeEnchantment(
                            new StringIdentifier("earthresist"), // FIXME: just for more sample enchantments
                            1,
                            15,
                            0,
                            10),
                    };
                    var repository = new InMemoryEnchantmentDefinitionRepository(
                        c.Resolve<IAttributeFilterer>(),
                        enchantmentDefinitions);
                    return repository;
                })
                .SingleInstance()
                .AsImplementedInterfaces();
            builder
                .RegisterType<RareEnchantmentGenerator>()
                .AsImplementedInterfaces()
                .SingleInstance();      
        }
    }
}
