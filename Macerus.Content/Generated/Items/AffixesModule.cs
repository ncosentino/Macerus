
using System;

using Autofac;

using Macerus.Content.Affixes;
using Macerus.Plugins.Features.GameObjects.Items.Affixes.Default;

using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Shared.Framework;

namespace Macerus.Content.Generated.Items
{
    public sealed class AffixesModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c =>
                {
                    var affixTemplate = c.Resolve<AffixTemplate>();
                    var affixDefinitions = new[]
                    {
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_1"),
                            0,
                            50,
                            new StringIdentifier("item_prefix_name_1"),
                            new StringIdentifier("item_suffix_name_1"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_1_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_1"),
                            0,
                            50,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_1_enchantment_0")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_2"),
                            5,
                            65,
                            new StringIdentifier("item_prefix_name_2"),
                            new StringIdentifier("item_suffix_name_2"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_2_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_2"),
                            5,
                            65,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_2_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_3"),
                            10,
                            80,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_3_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_4"),
                            15,
                            100,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_4_enchantment_0")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_5"),
                            20,
                            100,
                            new StringIdentifier("item_prefix_name_5"),
                            new StringIdentifier("item_suffix_name_5"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_5_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_5"),
                            20,
                            100,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_5_enchantment_0")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_6"),
                            30,
                            100,
                            new StringIdentifier("item_prefix_name_6"),
                            new StringIdentifier("item_suffix_name_6"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_6_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_6"),
                            30,
                            100,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_6_enchantment_0")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_7"),
                            40,
                            100,
                            null,
                            new StringIdentifier("item_suffix_name_7"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_7_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_7"),
                            40,
                            100,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_7_enchantment_0")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_8"),
                            50,
                            100,
                            new StringIdentifier("item_prefix_name_8"),
                            new StringIdentifier("item_suffix_name_8"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_8_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_8"),
                            50,
                            100,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_8_enchantment_0")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_9"),
                            65,
                            100,
                            new StringIdentifier("item_prefix_name_9"),
                            new StringIdentifier("item_suffix_name_9"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_9_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_9"),
                            65,
                            100,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_9_enchantment_0")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_10"),
                            80,
                            100,
                            new StringIdentifier("item_prefix_name_10"),
                            new StringIdentifier("item_suffix_name_10"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_10_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_10"),
                            80,
                            100,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_10_enchantment_0")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_11"),
                            0,
                            50,
                            new StringIdentifier("item_prefix_name_11"),
                            new StringIdentifier("item_suffix_name_11"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_11_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_11"),
                            0,
                            50,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_11_enchantment_0")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_12"),
                            5,
                            65,
                            new StringIdentifier("item_prefix_name_12"),
                            new StringIdentifier("item_suffix_name_12"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_12_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_12"),
                            5,
                            65,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_12_enchantment_0")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_13"),
                            10,
                            80,
                            new StringIdentifier("item_prefix_name_13"),
                            new StringIdentifier("item_suffix_name_13"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_13_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_13"),
                            10,
                            80,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_13_enchantment_0")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_14"),
                            15,
                            100,
                            new StringIdentifier("item_prefix_name_14"),
                            new StringIdentifier("item_suffix_name_14"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_14_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_14"),
                            15,
                            100,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_14_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_15"),
                            20,
                            100,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_15_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_16"),
                            30,
                            100,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_16_enchantment_0")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_17"),
                            40,
                            100,
                            new StringIdentifier("item_prefix_name_17"),
                            new StringIdentifier("item_suffix_name_17"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_17_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_17"),
                            40,
                            100,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_17_enchantment_0")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_18"),
                            50,
                            100,
                            new StringIdentifier("item_prefix_name_18"),
                            new StringIdentifier("item_suffix_name_18"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_18_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_18"),
                            50,
                            100,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_18_enchantment_0")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_19"),
                            65,
                            100,
                            new StringIdentifier("item_prefix_name_19"),
                            new StringIdentifier("item_suffix_name_19"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_19_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_19"),
                            65,
                            100,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_19_enchantment_0")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_20"),
                            80,
                            100,
                            new StringIdentifier("item_prefix_name_20"),
                            new StringIdentifier("item_suffix_name_20"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_20_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_20"),
                            80,
                            100,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_20_enchantment_0")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_21"),
                            0,
                            50,
                            new StringIdentifier("item_prefix_name_21"),
                            new StringIdentifier("item_suffix_name_21"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_21_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_21"),
                            0,
                            50,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_21_enchantment_0")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_22"),
                            5,
                            65,
                            new StringIdentifier("item_prefix_name_22"),
                            new StringIdentifier("item_suffix_name_22"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_22_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_22"),
                            5,
                            65,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_22_enchantment_0")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_23"),
                            10,
                            80,
                            new StringIdentifier("item_prefix_name_23"),
                            new StringIdentifier("item_suffix_name_23"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_23_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_23"),
                            10,
                            80,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_23_enchantment_0")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_24"),
                            15,
                            100,
                            new StringIdentifier("item_prefix_name_24"),
                            new StringIdentifier("item_suffix_name_24"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_24_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_24"),
                            15,
                            100,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_24_enchantment_0")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_25"),
                            20,
                            100,
                            new StringIdentifier("item_prefix_name_25"),
                            new StringIdentifier("item_suffix_name_25"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_25_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_25"),
                            20,
                            100,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_25_enchantment_0")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_26"),
                            30,
                            100,
                            new StringIdentifier("item_prefix_name_26"),
                            new StringIdentifier("item_suffix_name_26"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_26_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_26"),
                            30,
                            100,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_26_enchantment_0")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_27"),
                            40,
                            100,
                            new StringIdentifier("item_prefix_name_27"),
                            new StringIdentifier("item_suffix_name_27"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_27_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_27"),
                            40,
                            100,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_27_enchantment_0")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_28"),
                            50,
                            100,
                            new StringIdentifier("item_prefix_name_28"),
                            new StringIdentifier("item_suffix_name_28"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_28_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_28"),
                            50,
                            100,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_28_enchantment_0")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_29"),
                            65,
                            100,
                            new StringIdentifier("item_prefix_name_29"),
                            new StringIdentifier("item_suffix_name_29"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_29_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_29"),
                            65,
                            100,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_29_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_30"),
                            80,
                            100,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_30_enchantment_0")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_31"),
                            0,
                            50,
                            new StringIdentifier("item_prefix_name_31"),
                            new StringIdentifier("item_suffix_name_31"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_31_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_31"),
                            0,
                            50,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_31_enchantment_0")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_32"),
                            5,
                            65,
                            new StringIdentifier("item_prefix_name_32"),
                            new StringIdentifier("item_suffix_name_32"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_32_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_32"),
                            5,
                            65,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_32_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_33"),
                            10,
                            80,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_33_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_34"),
                            15,
                            100,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_34_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_35"),
                            20,
                            100,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_35_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_36"),
                            30,
                            100,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_36_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_37"),
                            40,
                            100,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_37_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_38"),
                            50,
                            100,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_38_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_39"),
                            65,
                            100,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_39_enchantment_0")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_40"),
                            80,
                            100,
                            new StringIdentifier("item_prefix_name_40"),
                            new StringIdentifier("item_suffix_name_40"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_40_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_40"),
                            80,
                            100,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_40_enchantment_0")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_41"),
                            0,
                            50,
                            new StringIdentifier("item_prefix_name_41"),
                            new StringIdentifier("item_suffix_name_41"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_41_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_41"),
                            0,
                            50,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_41_enchantment_0")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_42"),
                            5,
                            65,
                            new StringIdentifier("item_prefix_name_42"),
                            new StringIdentifier("item_suffix_name_42"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_42_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_42"),
                            5,
                            65,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_42_enchantment_0")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_43"),
                            10,
                            80,
                            new StringIdentifier("item_prefix_name_43"),
                            new StringIdentifier("item_suffix_name_43"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_43_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_43"),
                            10,
                            80,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_43_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_44"),
                            15,
                            100,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_44_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_45"),
                            20,
                            100,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_45_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_46"),
                            30,
                            100,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_46_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_47"),
                            40,
                            100,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_47_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_48"),
                            50,
                            100,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_48_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_49"),
                            65,
                            100,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_49_enchantment_0")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_50"),
                            80,
                            100,
                            new StringIdentifier("item_prefix_name_50"),
                            new StringIdentifier("item_suffix_name_50"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_50_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_50"),
                            80,
                            100,
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_50_enchantment_0")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_60"),
                            80,
                            100,
                            new StringIdentifier("item_prefix_name_60"),
                            new StringIdentifier("item_suffix_name_60"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_60_enchantment_0"),
                                new StringIdentifier("magic_item_affix_60_enchantment_1"),
                                new StringIdentifier("magic_item_affix_60_enchantment_2"),
                                new StringIdentifier("magic_item_affix_60_enchantment_3"),
                                new StringIdentifier("magic_item_affix_60_enchantment_4")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_61"),
                            0,
                            50,
                            new StringIdentifier("item_prefix_name_61"),
                            new StringIdentifier("item_suffix_name_61"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_61_enchantment_0")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_71"),
                            0,
                            50,
                            new StringIdentifier("item_prefix_name_71"),
                            new StringIdentifier("item_suffix_name_71"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_71_enchantment_0")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_81"),
                            0,
                            50,
                            new StringIdentifier("item_prefix_name_81"),
                            new StringIdentifier("item_suffix_name_81"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_81_enchantment_0"),
                                new StringIdentifier("magic_item_affix_81_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_82"),
                            5,
                            65,
                            new StringIdentifier("item_prefix_name_82"),
                            new StringIdentifier("item_suffix_name_82"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_82_enchantment_0"),
                                new StringIdentifier("magic_item_affix_82_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_83"),
                            10,
                            80,
                            new StringIdentifier("item_prefix_name_83"),
                            new StringIdentifier("item_suffix_name_83"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_83_enchantment_0"),
                                new StringIdentifier("magic_item_affix_83_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_84"),
                            15,
                            100,
                            new StringIdentifier("item_prefix_name_84"),
                            new StringIdentifier("item_suffix_name_84"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_84_enchantment_0"),
                                new StringIdentifier("magic_item_affix_84_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_85"),
                            20,
                            100,
                            new StringIdentifier("item_prefix_name_85"),
                            new StringIdentifier("item_suffix_name_85"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_85_enchantment_0"),
                                new StringIdentifier("magic_item_affix_85_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_86"),
                            30,
                            100,
                            new StringIdentifier("item_prefix_name_86"),
                            new StringIdentifier("item_suffix_name_86"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_86_enchantment_0"),
                                new StringIdentifier("magic_item_affix_86_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_87"),
                            40,
                            100,
                            new StringIdentifier("item_prefix_name_87"),
                            new StringIdentifier("item_suffix_name_87"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_87_enchantment_0"),
                                new StringIdentifier("magic_item_affix_87_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_88"),
                            50,
                            100,
                            new StringIdentifier("item_prefix_name_88"),
                            new StringIdentifier("item_suffix_name_88"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_88_enchantment_0"),
                                new StringIdentifier("magic_item_affix_88_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_89"),
                            65,
                            100,
                            new StringIdentifier("item_prefix_name_89"),
                            new StringIdentifier("item_suffix_name_89"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_89_enchantment_0"),
                                new StringIdentifier("magic_item_affix_89_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_90"),
                            80,
                            100,
                            new StringIdentifier("item_prefix_name_90"),
                            new StringIdentifier("item_suffix_name_90"),
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_90_enchantment_0"),
                                new StringIdentifier("magic_item_affix_90_enchantment_1")
                            })
                    };
                    var repository = new InMemoryAffixDefinitionRepository(
                        c.Resolve<Lazy<IAttributeFilterer>>(),
                        affixDefinitions);
                    return repository;
                })
                .SingleInstance()
                .AsImplementedInterfaces();
        }
    }
}