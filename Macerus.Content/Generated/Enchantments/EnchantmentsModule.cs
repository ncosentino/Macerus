
using Autofac;

using Macerus.Content.Enchantments;

using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Generation.InMemory;
using ProjectXyz.Shared.Framework;

namespace Macerus.Content.Generated.Enchantments
{
    public sealed class EnchantmentsModule : SingleRegistrationModule
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
                            new StringIdentifier("magic_item_affix_1_enchantment_0"),
                            new StringIdentifier("stat_10"), // STRENGTH
                            "+",
                            1,
                            3,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_1_enchantment_0"),
                            new StringIdentifier("stat_10"), // STRENGTH
                            "+",
                            1,
                            2,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_2_enchantment_0"),
                            new StringIdentifier("stat_10"), // STRENGTH
                            "+",
                            3,
                            5,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_2_enchantment_0"),
                            new StringIdentifier("stat_10"), // STRENGTH
                            "+",
                            2,
                            3,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_3_enchantment_0"),
                            new StringIdentifier("stat_10"), // STRENGTH
                            "+",
                            3,
                            5,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_4_enchantment_0"),
                            new StringIdentifier("stat_10"), // STRENGTH
                            "+",
                            5,
                            7,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_5_enchantment_0"),
                            new StringIdentifier("stat_10"), // STRENGTH
                            "+",
                            12,
                            20,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_5_enchantment_0"),
                            new StringIdentifier("stat_10"), // STRENGTH
                            "+",
                            7,
                            12,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_6_enchantment_0"),
                            new StringIdentifier("stat_10"), // STRENGTH
                            "+",
                            20,
                            30,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_6_enchantment_0"),
                            new StringIdentifier("stat_10"), // STRENGTH
                            "+",
                            12,
                            18,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_7_enchantment_0"),
                            new StringIdentifier("stat_10"), // STRENGTH
                            "+",
                            30,
                            40,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_7_enchantment_0"),
                            new StringIdentifier("stat_10"), // STRENGTH
                            "+",
                            18,
                            24,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_8_enchantment_0"),
                            new StringIdentifier("stat_10"), // STRENGTH
                            "+",
                            40,
                            50,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_8_enchantment_0"),
                            new StringIdentifier("stat_10"), // STRENGTH
                            "+",
                            24,
                            30,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_9_enchantment_0"),
                            new StringIdentifier("stat_10"), // STRENGTH
                            "+",
                            50,
                            60,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_9_enchantment_0"),
                            new StringIdentifier("stat_10"), // STRENGTH
                            "+",
                            30,
                            36,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_10_enchantment_0"),
                            new StringIdentifier("stat_10"), // STRENGTH
                            "+",
                            60,
                            70,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_10_enchantment_0"),
                            new StringIdentifier("stat_10"), // STRENGTH
                            "+",
                            36,
                            42,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_11_enchantment_0"),
                            new StringIdentifier("stat_11"), // DEXTERITY
                            "+",
                            1,
                            3,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_11_enchantment_0"),
                            new StringIdentifier("stat_11"), // DEXTERITY
                            "+",
                            1,
                            2,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_12_enchantment_0"),
                            new StringIdentifier("stat_11"), // DEXTERITY
                            "+",
                            3,
                            5,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_12_enchantment_0"),
                            new StringIdentifier("stat_11"), // DEXTERITY
                            "+",
                            2,
                            3,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_13_enchantment_0"),
                            new StringIdentifier("stat_11"), // DEXTERITY
                            "+",
                            5,
                            8,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_13_enchantment_0"),
                            new StringIdentifier("stat_11"), // DEXTERITY
                            "+",
                            3,
                            5,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_14_enchantment_0"),
                            new StringIdentifier("stat_11"), // DEXTERITY
                            "+",
                            8,
                            12,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_14_enchantment_0"),
                            new StringIdentifier("stat_11"), // DEXTERITY
                            "+",
                            5,
                            7,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_15_enchantment_0"),
                            new StringIdentifier("stat_11"), // DEXTERITY
                            "+",
                            7,
                            12,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_16_enchantment_0"),
                            new StringIdentifier("stat_11"), // DEXTERITY
                            "+",
                            12,
                            18,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_17_enchantment_0"),
                            new StringIdentifier("stat_11"), // DEXTERITY
                            "+",
                            30,
                            40,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_17_enchantment_0"),
                            new StringIdentifier("stat_11"), // DEXTERITY
                            "+",
                            18,
                            24,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_18_enchantment_0"),
                            new StringIdentifier("stat_11"), // DEXTERITY
                            "+",
                            40,
                            50,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_18_enchantment_0"),
                            new StringIdentifier("stat_11"), // DEXTERITY
                            "+",
                            24,
                            30,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_19_enchantment_0"),
                            new StringIdentifier("stat_11"), // DEXTERITY
                            "+",
                            50,
                            60,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_19_enchantment_0"),
                            new StringIdentifier("stat_11"), // DEXTERITY
                            "+",
                            30,
                            36,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_20_enchantment_0"),
                            new StringIdentifier("stat_11"), // DEXTERITY
                            "+",
                            60,
                            70,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_20_enchantment_0"),
                            new StringIdentifier("stat_11"), // DEXTERITY
                            "+",
                            36,
                            42,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_21_enchantment_0"),
                            new StringIdentifier("stat_12"), // INTELLIGENCE
                            "+",
                            1,
                            3,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_21_enchantment_0"),
                            new StringIdentifier("stat_12"), // INTELLIGENCE
                            "+",
                            1,
                            2,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_22_enchantment_0"),
                            new StringIdentifier("stat_12"), // INTELLIGENCE
                            "+",
                            3,
                            5,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_22_enchantment_0"),
                            new StringIdentifier("stat_12"), // INTELLIGENCE
                            "+",
                            2,
                            3,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_23_enchantment_0"),
                            new StringIdentifier("stat_12"), // INTELLIGENCE
                            "+",
                            5,
                            8,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_23_enchantment_0"),
                            new StringIdentifier("stat_12"), // INTELLIGENCE
                            "+",
                            3,
                            5,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_24_enchantment_0"),
                            new StringIdentifier("stat_12"), // INTELLIGENCE
                            "+",
                            8,
                            12,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_24_enchantment_0"),
                            new StringIdentifier("stat_12"), // INTELLIGENCE
                            "+",
                            5,
                            7,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_25_enchantment_0"),
                            new StringIdentifier("stat_12"), // INTELLIGENCE
                            "+",
                            12,
                            20,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_25_enchantment_0"),
                            new StringIdentifier("stat_12"), // INTELLIGENCE
                            "+",
                            7,
                            12,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_26_enchantment_0"),
                            new StringIdentifier("stat_12"), // INTELLIGENCE
                            "+",
                            20,
                            30,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_26_enchantment_0"),
                            new StringIdentifier("stat_12"), // INTELLIGENCE
                            "+",
                            12,
                            18,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_27_enchantment_0"),
                            new StringIdentifier("stat_12"), // INTELLIGENCE
                            "+",
                            30,
                            40,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_27_enchantment_0"),
                            new StringIdentifier("stat_12"), // INTELLIGENCE
                            "+",
                            18,
                            24,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_28_enchantment_0"),
                            new StringIdentifier("stat_12"), // INTELLIGENCE
                            "+",
                            40,
                            50,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_28_enchantment_0"),
                            new StringIdentifier("stat_12"), // INTELLIGENCE
                            "+",
                            24,
                            30,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_29_enchantment_0"),
                            new StringIdentifier("stat_12"), // INTELLIGENCE
                            "+",
                            50,
                            60,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_29_enchantment_0"),
                            new StringIdentifier("stat_12"), // INTELLIGENCE
                            "+",
                            30,
                            36,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_30_enchantment_0"),
                            new StringIdentifier("stat_12"), // INTELLIGENCE
                            "+",
                            36,
                            42,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_31_enchantment_0"),
                            new StringIdentifier("stat_13"), // VITALITY
                            "+",
                            1,
                            3,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_31_enchantment_0"),
                            new StringIdentifier("stat_13"), // VITALITY
                            "+",
                            1,
                            2,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_32_enchantment_0"),
                            new StringIdentifier("stat_13"), // VITALITY
                            "+",
                            3,
                            5,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_32_enchantment_0"),
                            new StringIdentifier("stat_13"), // VITALITY
                            "+",
                            2,
                            3,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_33_enchantment_0"),
                            new StringIdentifier("stat_13"), // VITALITY
                            "+",
                            3,
                            5,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_34_enchantment_0"),
                            new StringIdentifier("stat_13"), // VITALITY
                            "+",
                            5,
                            7,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_35_enchantment_0"),
                            new StringIdentifier("stat_13"), // VITALITY
                            "+",
                            7,
                            12,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_36_enchantment_0"),
                            new StringIdentifier("stat_13"), // VITALITY
                            "+",
                            12,
                            18,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_37_enchantment_0"),
                            new StringIdentifier("stat_13"), // VITALITY
                            "+",
                            18,
                            24,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_38_enchantment_0"),
                            new StringIdentifier("stat_13"), // VITALITY
                            "+",
                            24,
                            30,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_39_enchantment_0"),
                            new StringIdentifier("stat_13"), // VITALITY
                            "+",
                            30,
                            36,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_40_enchantment_0"),
                            new StringIdentifier("stat_13"), // VITALITY
                            "+",
                            60,
                            70,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_40_enchantment_0"),
                            new StringIdentifier("stat_13"), // VITALITY
                            "+",
                            36,
                            42,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_41_enchantment_0"),
                            new StringIdentifier("stat_14"), // SPEED
                            "+",
                            1,
                            3,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_41_enchantment_0"),
                            new StringIdentifier("stat_14"), // SPEED
                            "+",
                            1,
                            2,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_42_enchantment_0"),
                            new StringIdentifier("stat_14"), // SPEED
                            "+",
                            3,
                            5,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_42_enchantment_0"),
                            new StringIdentifier("stat_14"), // SPEED
                            "+",
                            2,
                            3,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_43_enchantment_0"),
                            new StringIdentifier("stat_14"), // SPEED
                            "+",
                            5,
                            8,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_43_enchantment_0"),
                            new StringIdentifier("stat_14"), // SPEED
                            "+",
                            3,
                            5,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_44_enchantment_0"),
                            new StringIdentifier("stat_14"), // SPEED
                            "+",
                            5,
                            7,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_45_enchantment_0"),
                            new StringIdentifier("stat_14"), // SPEED
                            "+",
                            7,
                            12,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_46_enchantment_0"),
                            new StringIdentifier("stat_14"), // SPEED
                            "+",
                            12,
                            18,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_47_enchantment_0"),
                            new StringIdentifier("stat_14"), // SPEED
                            "+",
                            18,
                            24,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_48_enchantment_0"),
                            new StringIdentifier("stat_14"), // SPEED
                            "+",
                            24,
                            30,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_49_enchantment_0"),
                            new StringIdentifier("stat_14"), // SPEED
                            "+",
                            30,
                            36,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_50_enchantment_0"),
                            new StringIdentifier("stat_14"), // SPEED
                            "+",
                            60,
                            70,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("rare_item_affix_50_enchantment_0"),
                            new StringIdentifier("stat_14"), // SPEED
                            "+",
                            36,
                            42,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_60_enchantment_0"),
                            new StringIdentifier("stat_10"), // STRENGTH
                            "+",
                            24,
                            28,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_60_enchantment_1"),
                            new StringIdentifier("stat_11"), // DEXTERITY
                            "+",
                            24,
                            28,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_60_enchantment_2"),
                            new StringIdentifier("stat_12"), // INTELLIGENCE
                            "+",
                            24,
                            28,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_60_enchantment_3"),
                            new StringIdentifier("stat_13"), // VITALITY
                            "+",
                            24,
                            28,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_60_enchantment_4"),
                            new StringIdentifier("stat_14"), // SPEED
                            "+",
                            24,
                            28,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_61_enchantment_0"),
                            new StringIdentifier("stat_1"), // LIFE_MAXIMUM
                            "+",
                            1,
                            5,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_71_enchantment_0"),
                            new StringIdentifier("stat_3"), // MANA_MAXIMUM
                            "+",
                            1,
                            10,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_80_enchantment_0"),
                            new StringIdentifier("stat_3"), // MANA_MAXIMUM
                            "+",
                            275,
                            445,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_81_enchantment_0"),
                            new StringIdentifier("stat_15"), // FIRE_DAMAGE_MIN
                            "+",
                            1,
                            3,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_81_enchantment_1"),
                            new StringIdentifier("stat_25"), // FIRE_DAMAGE_MAX
                            "+",
                            1,
                            4,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_82_enchantment_0"),
                            new StringIdentifier("stat_15"), // FIRE_DAMAGE_MIN
                            "+",
                            2,
                            4,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_82_enchantment_1"),
                            new StringIdentifier("stat_25"), // FIRE_DAMAGE_MAX
                            "+",
                            3,
                            8,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_83_enchantment_0"),
                            new StringIdentifier("stat_15"), // FIRE_DAMAGE_MIN
                            "+",
                            5,
                            12,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_83_enchantment_1"),
                            new StringIdentifier("stat_25"), // FIRE_DAMAGE_MAX
                            "+",
                            8,
                            20,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_84_enchantment_0"),
                            new StringIdentifier("stat_15"), // FIRE_DAMAGE_MIN
                            "+",
                            13,
                            32,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_84_enchantment_1"),
                            new StringIdentifier("stat_25"), // FIRE_DAMAGE_MAX
                            "+",
                            21,
                            52,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_85_enchantment_0"),
                            new StringIdentifier("stat_15"), // FIRE_DAMAGE_MIN
                            "+",
                            34,
                            84,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_85_enchantment_1"),
                            new StringIdentifier("stat_25"), // FIRE_DAMAGE_MAX
                            "+",
                            55,
                            136,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_86_enchantment_0"),
                            new StringIdentifier("stat_15"), // FIRE_DAMAGE_MIN
                            "+",
                            89,
                            220,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_86_enchantment_1"),
                            new StringIdentifier("stat_25"), // FIRE_DAMAGE_MAX
                            "+",
                            144,
                            356,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_87_enchantment_0"),
                            new StringIdentifier("stat_15"), // FIRE_DAMAGE_MIN
                            "+",
                            233,
                            576,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_87_enchantment_1"),
                            new StringIdentifier("stat_25"), // FIRE_DAMAGE_MAX
                            "+",
                            377,
                            932,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_88_enchantment_0"),
                            new StringIdentifier("stat_15"), // FIRE_DAMAGE_MIN
                            "+",
                            610,
                            1508,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_88_enchantment_1"),
                            new StringIdentifier("stat_25"), // FIRE_DAMAGE_MAX
                            "+",
                            987,
                            2440,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_89_enchantment_0"),
                            new StringIdentifier("stat_15"), // FIRE_DAMAGE_MIN
                            "+",
                            1597,
                            3948,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_89_enchantment_1"),
                            new StringIdentifier("stat_25"), // FIRE_DAMAGE_MAX
                            "+",
                            2584,
                            6388,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_90_enchantment_0"),
                            new StringIdentifier("stat_15"), // FIRE_DAMAGE_MIN
                            "+",
                            4181,
                            10336,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("magic_item_affix_90_enchantment_1"),
                            new StringIdentifier("stat_25"), // FIRE_DAMAGE_MAX
                            "+",
                            6765,
                            16724,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("unique_item_1_enchantment_0"),
                            new StringIdentifier("stat_16"), // ICE_DAMAGE_MIN
                            "+",
                            100,
                            120,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("unique_item_1_enchantment_1"),
                            new StringIdentifier("stat_26"), // ICE_DAMAGE_MAX
                            "+",
                            100,
                            120,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("unique_item_1_enchantment_2"),
                            new StringIdentifier("stat_16"), // ICE_DAMAGE_MIN
                            "*",
                            10,
                            20,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("unique_item_1_enchantment_3"),
                            new StringIdentifier("stat_26"), // ICE_DAMAGE_MAX
                            "*",
                            10,
                            20,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("unique_item_1_enchantment_4"),
                            new StringIdentifier("stat_19"), // PHYSICAL_DAMAGE_MIN
                            "+",
                            50,
                            75,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("unique_item_1_enchantment_5"),
                            new StringIdentifier("stat_29"), // PHYSICAL_DAMAGE_MAX
                            "+",
                            50,
                            75,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("unique_item_1_enchantment_6"),
                            new StringIdentifier("stat_19"), // PHYSICAL_DAMAGE_MIN
                            "*",
                            5,
                            10,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("unique_item_1_enchantment_7"),
                            new StringIdentifier("stat_29"), // PHYSICAL_DAMAGE_MAX
                            "*",
                            5,
                            10,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("unique_item_1_enchantment_8"),
                            new StringIdentifier("stat_107"), // ICE_RESIST_PENETRATION
                            "+",
                            5,
                            8,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("unique_item_1_enchantment_9"),
                            new StringIdentifier("stat_35"), // FIRE_RESIST
                            "-",
                            20,
                            25,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("unique_item_1_enchantment_10"),
                            new StringIdentifier("stat_64"), // ATTACK_SPEED
                            "-",
                            0.1,
                            0.15,
                            2),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("unique_item_17_enchantment_0"),
                            new StringIdentifier("stat_182"), // ITEM_ICON_TINT_RED
                            "+",
                            0.25,
                            0.25,
                            2),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("unique_item_17_enchantment_1"),
                            new StringIdentifier("stat_15"), // FIRE_DAMAGE_MIN
                            "+",
                            200,
                            220,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("unique_item_17_enchantment_2"),
                            new StringIdentifier("stat_25"), // FIRE_DAMAGE_MAX
                            "+",
                            200,
                            220,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("unique_item_17_enchantment_3"),
                            new StringIdentifier("stat_15"), // FIRE_DAMAGE_MIN
                            "*",
                            25,
                            30,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("unique_item_17_enchantment_4"),
                            new StringIdentifier("stat_25"), // FIRE_DAMAGE_MAX
                            "*",
                            25,
                            30,
                            0),
                        enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier("unique_item_17_enchantment_5"),
                            new StringIdentifier("stat_106"), // FIRE_RESIST_PENETRATION
                            "+",
                            10,
                            12,
                            0)
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