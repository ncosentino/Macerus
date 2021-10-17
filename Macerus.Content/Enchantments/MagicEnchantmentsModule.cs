using Autofac;

using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Generation.InMemory;
using ProjectXyz.Shared.Framework;

namespace Macerus.Content.Enchantments
{
    public sealed class MagicEnchantmentsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c =>
                {
                    var enchantmentTemplate = c.Resolve<EnchantmentTemplate>();

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
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("lively-ench"),
                            new IntIdentifier(1), // max life
                            1,
                            15),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("hearty-ench"),
                            new IntIdentifier(1), // max life
                            16,
                            50),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic-ench"),
                            new IntIdentifier(3), // max mana
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
