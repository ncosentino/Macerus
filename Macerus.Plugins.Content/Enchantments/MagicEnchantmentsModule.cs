using Autofac;

using Macerus.Plugins.Features.GameObjects.Items.Generation.Magic.Enchantments;

using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Generation.InMemory;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Content.Enchantments
{
    public sealed class MagicEnchantmentsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c =>
                {
                    var enchantmentTemplate = new EnchantmentTemplate(c.Resolve<ICalculationPriorityFactory>());

                    // FIXME: it's totally a smell that we have a stat
                    // definition ID per enchantment when we have things like
                    // expressions that are responsible for that stat ID.
                    // notice the double reference to a stat ID per
                    // enchantment (but i guess this is because the expression
                    // must get assigned to only one stat id).
                    // See this class for related comments:
                    // ProjectXyz.Shared.Game.GameObjects.Enchantments.EnchantmentFactory
                    var enchantmentDefinitions = new[]
                    {
                        enchantmentTemplate.CreateMagicRangeEnchantment(
                            new IntIdentifier(1), // max life
                            Affixes.Prefixes.Lively,
                            Affixes.Suffixes.OfLife,
                            1,
                            15,
                            0,
                            20),
                        enchantmentTemplate.CreateMagicRangeEnchantment(
                            new IntIdentifier(1), // max life
                            Affixes.Prefixes.Hearty,
                            Affixes.Suffixes.OfHeart,
                            16,
                            50,
                            10,
                            30),
                        enchantmentTemplate.CreateMagicRangeEnchantment(
                            new IntIdentifier(3), // max mana
                            Affixes.Prefixes.Magic,
                            Affixes.Suffixes.OfMana,
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
                .RegisterType<MagicEnchantmentGenerator>()
                .AsImplementedInterfaces()
                .SingleInstance();      
        }
    }
}
