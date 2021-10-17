using Autofac;

using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Generation.InMemory;
using ProjectXyz.Shared.Framework;

namespace Macerus.Content.Enchantments
{
    public sealed class RareEnchantmentsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c =>
                {
                    var enchantmentTemplate = c.Resolve<EnchantmentTemplate>();
                    var enchantmentDefinitions = new[]
                    {
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare-life-ench"),
                            new IntIdentifier(1), // max life
                            1,
                            15),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare-mana-ench"),
                            new IntIdentifier(3), // max mana
                            1,
                            15),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare-fire-res-ench"),
                            new IntIdentifier(34),
                            1,
                            15),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare-ice-res-ench"),
                            new IntIdentifier(35),
                            1,
                            15),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare-water-res-ench"),
                            new IntIdentifier(36),
                            1,
                            15),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare-lightning-res-ench"),
                            new IntIdentifier(37),
                            1,
                            15),
                    };
                    var repository = new InMemoryEnchantmentDefinitionRepository(
                        c.Resolve<IAttributeFilterer>(),
                        enchantmentDefinitions);
                    return repository;
                })
                .SingleInstance()
                .AsImplementedInterfaces();   
        }
    }
}
