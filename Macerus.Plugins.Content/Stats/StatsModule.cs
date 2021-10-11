using System.Collections.Generic;

using Autofac;

using Macerus.Plugins.Features.Combat.Api;
using Macerus.Plugins.Features.GameObjects.Actors;

using ProjectXyz.Api.Framework;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Stats.Default;
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
                    var actorIdentifiers = c.Resolve<IMacerusActorIdentifiers>();

                    // FIXME: these are all temporary just for testing so we
                    // don't need to keep updating the database while we are
                    // iterating on features
                    var mapping = new Dictionary<IIdentifier, string>()
                    {
                        // game-system specific
                        // combat
                        //[combatTeamIdentifiers.CombatTeamStatDefinitionId] = "COMBAT_TEAM",
                        //[new StringIdentifier("attack-speed")] = "ATTACK_SPEED",
                        // general
                        //[new StringIdentifier("speed")] = "SPEED",
                        //[actorIdentifiers.MoveDistancePerTurnCurrentStatDefinitionId] = "MOVE_DISTANCE_PER_TURN_CURRENT",
                        //[actorIdentifiers.MoveDistancePerTurnTotalStatDefinitionId] = "MOVE_DISTANCE_PER_TURN_TOTAL",
                        //[actorIdentifiers.MoveDiagonallyStatDefinitionId] = "MOVE_DIAGONALLY",
                        // defensive
                        //[new StringIdentifier("armor")] = "ARMOR",
                        //[new StringIdentifier("dodge")] = "DODGE",
                        //[new StringIdentifier("block")] = "BLOCK",
                        // damage types
                        //[new StringIdentifier("firedmg")] = "FIRE_DAMAGE",
                        //[new StringIdentifier("icedmg")] = "ICE_DAMAGE",
                        //[new StringIdentifier("waterdmg")] = "WATER_DAMAGE",
                        //[new StringIdentifier("lightningdmg")] = "LIGHTNING_DAMAGE",
                        //[new StringIdentifier("earthdmg")] = "EARTH_DAMAGE",
                        //[new StringIdentifier("poisondmg")] = "POISON_DAMAGE",
                        //[new StringIdentifier("darkdmg")] = "DARK_DAMAGE",
                        //[new StringIdentifier("lightdmg")] = "LIGHT_DAMAGE",
                        //[new StringIdentifier("physicaldmg")] = "PHYSICAL_DAMAGE",
                        //[new StringIdentifier("magicdmg")] = "MAGIC_DAMAGE",
                        // resistances
                        //[new StringIdentifier("fireresist")] = "FIRE_RESIST",
                        //[new StringIdentifier("fireresistmin")] = "FIRE_RESIST_MIN",
                        //[new StringIdentifier("fireresistmax")] = "FIRE_RESIST_MAX",
                        //[new StringIdentifier("iceresist")] = "ICE_RESIST",
                        //[new StringIdentifier("iceresistmin")] = "ICE_RESIST_MIN",
                        //[new StringIdentifier("iceresistmax")] = "ICE_RESIST_MAX",
                        //[new StringIdentifier("waterresist")] = "WATER_RESIST",
                        //[new StringIdentifier("waterresistmin")] = "WATER_RESIST_MIN",
                        //[new StringIdentifier("waterresistmax")] = "WATER_RESIST_MAX",
                        //[new StringIdentifier("lightningresist")] = "LIGHTNING_RESIST",
                        //[new StringIdentifier("lightningresistmin")] = "LIGHTNING_RESIST_MIN",
                        //[new StringIdentifier("lightningresistmax")] = "LIGHTNING_RESIST_MAX",
                        //[new StringIdentifier("earthresist")] = "EARTH_RESIST",
                        //[new StringIdentifier("earthresistmin")] = "EARTH_RESIST_MIN",
                        //[new StringIdentifier("earthresistmax")] = "EARTH_RESIST_MAX",
                        //[new StringIdentifier("poisonresist")] = "POISON_RESIST",
                        //[new StringIdentifier("poisonresistmin")] = "POISON_RESIST_MIN",
                        //[new StringIdentifier("poisonresistmax")] = "POISON_RESIST_MAX",
                        //[new StringIdentifier("darkresist")] = "DARK_RESIST",
                        //[new StringIdentifier("darkresistmin")] = "DARK_RESIST_MIN",
                        //[new StringIdentifier("darkresistmax")] = "DARK_RESIST_MAX",
                        //[new StringIdentifier("lightresist")] = "LIGHT_RESIST",
                        //[new StringIdentifier("lightresistmin")] = "LIGHT_RESIST_MIN",
                        //[new StringIdentifier("lightresistmax")] = "LIGHT_RESIST_MAX",
                        //[new StringIdentifier("physicalresist")] = "PHYSICAL_RESIST",
                        //[new StringIdentifier("physicalresistmin")] = "PHYSICAL_RESIST_MIN",
                        //[new StringIdentifier("physicalresistmax")] = "PHYSICAL_RESIST_MAX",
                        //[new StringIdentifier("magicresist")] = "MAGIC_RESIST",
                        //[new StringIdentifier("magicresistmin")] = "MAGIC_RESIST_MIN",
                        //[new StringIdentifier("magicresistmax")] = "MAGIC_RESIST_MAX",
                        // summoning
                        [new StringIdentifier("maximum_summon_skeletons")] = "SUMMON_SKELETONS_MAX",
                    };
                    var statDefinitionToTermRepository = new InMemoryStatDefinitionToTermMappingRepository(mapping);
                    return statDefinitionToTermRepository;
                })
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}