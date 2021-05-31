using System.Collections.Generic;

using Autofac;

using Macerus.Plugins.Features.Combat.Api;

using ProjectXyz.Api.Framework;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Stats;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Content.Stats
{
    public sealed class StatsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<StatDefinitionIdToBoundsMappingRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<StatDefinitionToTermMappingRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .Register(c =>
                {
                    var combatTeamIdentifiers = c.Resolve<ICombatTeamIdentifiers>();

                    // FIXME: these are all temporary just for testing so we
                    // don't need to keep updating the database while we are
                    // iterating on features
                    var mapping = new Dictionary<IIdentifier, string>()
                    {
                        // game-system specific
                        [combatTeamIdentifiers.CombatTeamStatDefinitionId] = "COMBAT_TEAM",
                        // general
                        [new StringIdentifier("speed")] = "SPEED",
                        // defensive
                        [new StringIdentifier("armor")] = "ARMOR",
                        [new StringIdentifier("dodge")] = "DODGE",
                        [new StringIdentifier("block")] = "BLOCK",
                        // damage types
                        [new StringIdentifier("firedmg")] = "FIRE_DAMAGE",
                        [new StringIdentifier("icedmg")] = "ICE_DAMAGE",
                        [new StringIdentifier("waterdmg")] = "WATER_DAMAGE",
                        [new StringIdentifier("lightningdmg")] = "LIGHTNING_DAMAGE",
                        [new StringIdentifier("earthdmg")] = "EARTH_DAMAGE",
                        [new StringIdentifier("poisondmg")] = "POISON_DAMAGE",
                        [new StringIdentifier("darkdmg")] = "DARK_DAMAGE",
                        [new StringIdentifier("lightdmg")] = "LIGHT_DAMAGE",
                        // resistances
                        [new StringIdentifier("fireresist")] = "FIRE_RESIST",
                        [new StringIdentifier("iceresist")] = "ICE_RESIST",
                        [new StringIdentifier("waterresist")] = "WATER_RESIST",
                        [new StringIdentifier("lightningresist")] = "LIGHTNING_RESIST",
                        [new StringIdentifier("earthresist")] = "EARTH_RESIST",
                        [new StringIdentifier("poisonresist")] = "POISON_RESIST",
                        [new StringIdentifier("darkresist")] = "DARK_RESIST",
                        [new StringIdentifier("lightresist")] = "LIGHT_RESIST",
                    };
                    var statDefinitionToTermRepository = new InMemoryStatDefinitionToTermMappingRepository(mapping);
                    return statDefinitionToTermRepository;
                })
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}