
using System;

using Autofac;

using Macerus.Content.Affixes;
using Macerus.Plugins.Features.GameObjects.Items.Affixes.Default;

using ProjectXyz.Api.Framework;
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
                            new StringIdentifier("1"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_1_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_1"),
                            0,
                            50,
                            new StringIdentifier("1"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
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
                            new StringIdentifier("1"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_2_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_2"),
                            5,
                            65,
                            new StringIdentifier("1"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_2_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_3"),
                            10,
                            80,
                            new StringIdentifier("1"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_3_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_4"),
                            15,
                            100,
                            new StringIdentifier("1"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
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
                            new StringIdentifier("1"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_5_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_5"),
                            20,
                            100,
                            new StringIdentifier("1"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
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
                            new StringIdentifier("1"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_6_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_6"),
                            30,
                            100,
                            new StringIdentifier("1"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
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
                            new StringIdentifier("1"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_7_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_7"),
                            40,
                            100,
                            new StringIdentifier("1"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
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
                            new StringIdentifier("1"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_8_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_8"),
                            50,
                            100,
                            new StringIdentifier("1"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
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
                            new StringIdentifier("1"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_9_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_9"),
                            65,
                            100,
                            new StringIdentifier("1"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
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
                            new StringIdentifier("1"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_10_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_10"),
                            80,
                            100,
                            new StringIdentifier("1"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
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
                            new StringIdentifier("2"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_11_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_11"),
                            0,
                            50,
                            new StringIdentifier("2"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
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
                            new StringIdentifier("2"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_12_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_12"),
                            5,
                            65,
                            new StringIdentifier("2"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
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
                            new StringIdentifier("2"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_13_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_13"),
                            10,
                            80,
                            new StringIdentifier("2"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
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
                            new StringIdentifier("2"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_14_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_14"),
                            15,
                            100,
                            new StringIdentifier("2"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_14_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_15"),
                            20,
                            100,
                            new StringIdentifier("2"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_15_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_16"),
                            30,
                            100,
                            new StringIdentifier("2"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
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
                            new StringIdentifier("2"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_17_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_17"),
                            40,
                            100,
                            new StringIdentifier("2"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
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
                            new StringIdentifier("2"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_18_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_18"),
                            50,
                            100,
                            new StringIdentifier("2"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
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
                            new StringIdentifier("2"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_19_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_19"),
                            65,
                            100,
                            new StringIdentifier("2"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
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
                            new StringIdentifier("2"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_20_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_20"),
                            80,
                            100,
                            new StringIdentifier("2"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
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
                            new StringIdentifier("3"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_21_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_21"),
                            0,
                            50,
                            new StringIdentifier("3"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
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
                            new StringIdentifier("3"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_22_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_22"),
                            5,
                            65,
                            new StringIdentifier("3"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
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
                            new StringIdentifier("3"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_23_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_23"),
                            10,
                            80,
                            new StringIdentifier("3"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
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
                            new StringIdentifier("3"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_24_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_24"),
                            15,
                            100,
                            new StringIdentifier("3"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
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
                            new StringIdentifier("3"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_25_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_25"),
                            20,
                            100,
                            new StringIdentifier("3"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
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
                            new StringIdentifier("3"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_26_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_26"),
                            30,
                            100,
                            new StringIdentifier("3"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
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
                            new StringIdentifier("3"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_27_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_27"),
                            40,
                            100,
                            new StringIdentifier("3"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
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
                            new StringIdentifier("3"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_28_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_28"),
                            50,
                            100,
                            new StringIdentifier("3"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
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
                            new StringIdentifier("3"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_29_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_29"),
                            65,
                            100,
                            new StringIdentifier("3"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_29_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_30"),
                            80,
                            100,
                            new StringIdentifier("3"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
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
                            new StringIdentifier("4"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_31_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_31"),
                            0,
                            50,
                            new StringIdentifier("4"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
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
                            new StringIdentifier("4"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_32_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_32"),
                            5,
                            65,
                            new StringIdentifier("4"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_32_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_33"),
                            10,
                            80,
                            new StringIdentifier("4"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_33_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_34"),
                            15,
                            100,
                            new StringIdentifier("4"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_34_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_35"),
                            20,
                            100,
                            new StringIdentifier("4"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_35_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_36"),
                            30,
                            100,
                            new StringIdentifier("4"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_36_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_37"),
                            40,
                            100,
                            new StringIdentifier("4"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_37_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_38"),
                            50,
                            100,
                            new StringIdentifier("4"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_38_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_39"),
                            65,
                            100,
                            new StringIdentifier("4"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
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
                            new StringIdentifier("4"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_40_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_40"),
                            80,
                            100,
                            new StringIdentifier("4"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
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
                            new StringIdentifier("5"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_41_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_41"),
                            0,
                            50,
                            new StringIdentifier("5"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
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
                            new StringIdentifier("5"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_42_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_42"),
                            5,
                            65,
                            new StringIdentifier("5"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
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
                            new StringIdentifier("5"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_43_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_43"),
                            10,
                            80,
                            new StringIdentifier("5"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_43_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_44"),
                            15,
                            100,
                            new StringIdentifier("5"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_44_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_45"),
                            20,
                            100,
                            new StringIdentifier("5"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_45_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_46"),
                            30,
                            100,
                            new StringIdentifier("5"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_46_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_47"),
                            40,
                            100,
                            new StringIdentifier("5"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_47_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_48"),
                            50,
                            100,
                            new StringIdentifier("5"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_48_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_49"),
                            65,
                            100,
                            new StringIdentifier("5"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
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
                            new StringIdentifier("5"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_50_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_50"),
                            80,
                            100,
                            new StringIdentifier("5"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_50_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_51"),
                            0,
                            50,
                            new StringIdentifier("6"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_51_enchantment_0"),
                                new StringIdentifier("rare_item_affix_51_enchantment_1"),
                                new StringIdentifier("rare_item_affix_51_enchantment_2"),
                                new StringIdentifier("rare_item_affix_51_enchantment_3"),
                                new StringIdentifier("rare_item_affix_51_enchantment_4")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_52"),
                            5,
                            65,
                            new StringIdentifier("6"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_52_enchantment_0"),
                                new StringIdentifier("rare_item_affix_52_enchantment_1"),
                                new StringIdentifier("rare_item_affix_52_enchantment_2"),
                                new StringIdentifier("rare_item_affix_52_enchantment_3"),
                                new StringIdentifier("rare_item_affix_52_enchantment_4")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_53"),
                            10,
                            80,
                            new StringIdentifier("6"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_53_enchantment_0"),
                                new StringIdentifier("rare_item_affix_53_enchantment_1"),
                                new StringIdentifier("rare_item_affix_53_enchantment_2"),
                                new StringIdentifier("rare_item_affix_53_enchantment_3"),
                                new StringIdentifier("rare_item_affix_53_enchantment_4")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_54"),
                            15,
                            100,
                            new StringIdentifier("6"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_54_enchantment_0"),
                                new StringIdentifier("rare_item_affix_54_enchantment_1"),
                                new StringIdentifier("rare_item_affix_54_enchantment_2"),
                                new StringIdentifier("rare_item_affix_54_enchantment_3"),
                                new StringIdentifier("rare_item_affix_54_enchantment_4")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_55"),
                            20,
                            100,
                            new StringIdentifier("6"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_55_enchantment_0"),
                                new StringIdentifier("rare_item_affix_55_enchantment_1"),
                                new StringIdentifier("rare_item_affix_55_enchantment_2"),
                                new StringIdentifier("rare_item_affix_55_enchantment_3"),
                                new StringIdentifier("rare_item_affix_55_enchantment_4")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_56"),
                            30,
                            100,
                            new StringIdentifier("6"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_56_enchantment_0"),
                                new StringIdentifier("rare_item_affix_56_enchantment_1"),
                                new StringIdentifier("rare_item_affix_56_enchantment_2"),
                                new StringIdentifier("rare_item_affix_56_enchantment_3"),
                                new StringIdentifier("rare_item_affix_56_enchantment_4")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_57"),
                            40,
                            100,
                            new StringIdentifier("6"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_57_enchantment_0"),
                                new StringIdentifier("rare_item_affix_57_enchantment_1"),
                                new StringIdentifier("rare_item_affix_57_enchantment_2"),
                                new StringIdentifier("rare_item_affix_57_enchantment_3"),
                                new StringIdentifier("rare_item_affix_57_enchantment_4")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_58"),
                            50,
                            100,
                            new StringIdentifier("6"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_58_enchantment_0"),
                                new StringIdentifier("rare_item_affix_58_enchantment_1"),
                                new StringIdentifier("rare_item_affix_58_enchantment_2"),
                                new StringIdentifier("rare_item_affix_58_enchantment_3"),
                                new StringIdentifier("rare_item_affix_58_enchantment_4")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_59"),
                            65,
                            100,
                            new StringIdentifier("6"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_59_enchantment_0"),
                                new StringIdentifier("rare_item_affix_59_enchantment_1"),
                                new StringIdentifier("rare_item_affix_59_enchantment_2"),
                                new StringIdentifier("rare_item_affix_59_enchantment_3"),
                                new StringIdentifier("rare_item_affix_59_enchantment_4")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_60"),
                            80,
                            100,
                            new StringIdentifier("item_prefix_name_60"),
                            new StringIdentifier("item_suffix_name_60"),
                            new StringIdentifier("6"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_60_enchantment_0"),
                                new StringIdentifier("magic_item_affix_60_enchantment_1"),
                                new StringIdentifier("magic_item_affix_60_enchantment_2"),
                                new StringIdentifier("magic_item_affix_60_enchantment_3"),
                                new StringIdentifier("magic_item_affix_60_enchantment_4")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_60"),
                            80,
                            100,
                            new StringIdentifier("6"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_60_enchantment_0"),
                                new StringIdentifier("rare_item_affix_60_enchantment_1"),
                                new StringIdentifier("rare_item_affix_60_enchantment_2"),
                                new StringIdentifier("rare_item_affix_60_enchantment_3"),
                                new StringIdentifier("rare_item_affix_60_enchantment_4")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_61"),
                            0,
                            50,
                            new StringIdentifier("item_prefix_name_61"),
                            new StringIdentifier("item_suffix_name_61"),
                            new StringIdentifier("7"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_61_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_61"),
                            0,
                            50,
                            new StringIdentifier("7"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_61_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_62"),
                            5,
                            65,
                            new StringIdentifier("7"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_62_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_63"),
                            10,
                            80,
                            new StringIdentifier("7"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_63_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_64"),
                            15,
                            100,
                            new StringIdentifier("7"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_64_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_65"),
                            20,
                            100,
                            new StringIdentifier("7"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_65_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_66"),
                            30,
                            100,
                            new StringIdentifier("7"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_66_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_67"),
                            40,
                            100,
                            new StringIdentifier("7"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_67_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_68"),
                            50,
                            100,
                            new StringIdentifier("7"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_68_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_69"),
                            65,
                            100,
                            new StringIdentifier("7"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_69_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_70"),
                            80,
                            100,
                            new StringIdentifier("7"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_70_enchantment_0")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_71"),
                            0,
                            50,
                            new StringIdentifier("item_prefix_name_71"),
                            new StringIdentifier("item_suffix_name_71"),
                            new StringIdentifier("8"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_71_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_71"),
                            0,
                            50,
                            new StringIdentifier("8"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_71_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_72"),
                            5,
                            65,
                            new StringIdentifier("8"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_72_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_73"),
                            10,
                            80,
                            new StringIdentifier("8"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_73_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_74"),
                            15,
                            100,
                            new StringIdentifier("8"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_74_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_75"),
                            20,
                            100,
                            new StringIdentifier("8"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_75_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_76"),
                            30,
                            100,
                            new StringIdentifier("8"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_76_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_77"),
                            40,
                            100,
                            new StringIdentifier("8"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_77_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_78"),
                            50,
                            100,
                            new StringIdentifier("8"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_78_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_79"),
                            65,
                            100,
                            new StringIdentifier("8"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_79_enchantment_0")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_80"),
                            80,
                            100,
                            new StringIdentifier("item_prefix_name_80"),
                            new StringIdentifier("item_suffix_name_80"),
                            new StringIdentifier("8"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_80_enchantment_0")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_80"),
                            80,
                            100,
                            new StringIdentifier("8"),
                            new IIdentifier[]
                            {
                                // no tag filter
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_80_enchantment_0")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_81"),
                            0,
                            50,
                            new StringIdentifier("item_prefix_name_81"),
                            new StringIdentifier("item_suffix_name_81"),
                            new StringIdentifier("9"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_81_enchantment_0"),
                                new StringIdentifier("magic_item_affix_81_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_81"),
                            0,
                            50,
                            new StringIdentifier("9"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_81_enchantment_0"),
                                new StringIdentifier("rare_item_affix_81_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_82"),
                            5,
                            65,
                            new StringIdentifier("item_prefix_name_82"),
                            new StringIdentifier("item_suffix_name_82"),
                            new StringIdentifier("9"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_82_enchantment_0"),
                                new StringIdentifier("magic_item_affix_82_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_82"),
                            5,
                            65,
                            new StringIdentifier("9"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_82_enchantment_0"),
                                new StringIdentifier("rare_item_affix_82_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_83"),
                            10,
                            80,
                            new StringIdentifier("item_prefix_name_83"),
                            new StringIdentifier("item_suffix_name_83"),
                            new StringIdentifier("9"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_83_enchantment_0"),
                                new StringIdentifier("magic_item_affix_83_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_83"),
                            10,
                            80,
                            new StringIdentifier("9"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_83_enchantment_0"),
                                new StringIdentifier("rare_item_affix_83_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_84"),
                            15,
                            100,
                            new StringIdentifier("item_prefix_name_84"),
                            new StringIdentifier("item_suffix_name_84"),
                            new StringIdentifier("9"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_84_enchantment_0"),
                                new StringIdentifier("magic_item_affix_84_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_84"),
                            15,
                            100,
                            new StringIdentifier("9"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_84_enchantment_0"),
                                new StringIdentifier("rare_item_affix_84_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_85"),
                            20,
                            100,
                            new StringIdentifier("item_prefix_name_85"),
                            new StringIdentifier("item_suffix_name_85"),
                            new StringIdentifier("9"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_85_enchantment_0"),
                                new StringIdentifier("magic_item_affix_85_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_85"),
                            20,
                            100,
                            new StringIdentifier("9"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_85_enchantment_0"),
                                new StringIdentifier("rare_item_affix_85_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_86"),
                            30,
                            100,
                            new StringIdentifier("item_prefix_name_86"),
                            new StringIdentifier("item_suffix_name_86"),
                            new StringIdentifier("9"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_86_enchantment_0"),
                                new StringIdentifier("magic_item_affix_86_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_86"),
                            30,
                            100,
                            new StringIdentifier("9"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_86_enchantment_0"),
                                new StringIdentifier("rare_item_affix_86_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_87"),
                            40,
                            100,
                            new StringIdentifier("item_prefix_name_87"),
                            new StringIdentifier("item_suffix_name_87"),
                            new StringIdentifier("9"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_87_enchantment_0"),
                                new StringIdentifier("magic_item_affix_87_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_87"),
                            40,
                            100,
                            new StringIdentifier("9"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_87_enchantment_0"),
                                new StringIdentifier("rare_item_affix_87_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_88"),
                            50,
                            100,
                            new StringIdentifier("item_prefix_name_88"),
                            new StringIdentifier("item_suffix_name_88"),
                            new StringIdentifier("9"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_88_enchantment_0"),
                                new StringIdentifier("magic_item_affix_88_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_88"),
                            50,
                            100,
                            new StringIdentifier("9"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_88_enchantment_0"),
                                new StringIdentifier("rare_item_affix_88_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_89"),
                            65,
                            100,
                            new StringIdentifier("item_prefix_name_89"),
                            new StringIdentifier("item_suffix_name_89"),
                            new StringIdentifier("9"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_89_enchantment_0"),
                                new StringIdentifier("magic_item_affix_89_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_89"),
                            65,
                            100,
                            new StringIdentifier("9"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_89_enchantment_0"),
                                new StringIdentifier("rare_item_affix_89_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_90"),
                            80,
                            100,
                            new StringIdentifier("item_prefix_name_90"),
                            new StringIdentifier("item_suffix_name_90"),
                            new StringIdentifier("9"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_90_enchantment_0"),
                                new StringIdentifier("magic_item_affix_90_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_90"),
                            80,
                            100,
                            new StringIdentifier("9"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_90_enchantment_0"),
                                new StringIdentifier("rare_item_affix_90_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_91"),
                            0,
                            0,
                            new StringIdentifier("item_prefix_name_91"),
                            new StringIdentifier("item_suffix_name_91"),
                            new StringIdentifier("10"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_91_enchantment_0"),
                                new StringIdentifier("magic_item_affix_91_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_91"),
                            0,
                            0,
                            new StringIdentifier("10"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_91_enchantment_0"),
                                new StringIdentifier("rare_item_affix_91_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_92"),
                            0,
                            0,
                            new StringIdentifier("item_prefix_name_92"),
                            new StringIdentifier("item_suffix_name_92"),
                            new StringIdentifier("10"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_92_enchantment_0"),
                                new StringIdentifier("magic_item_affix_92_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_92"),
                            0,
                            0,
                            new StringIdentifier("10"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_92_enchantment_0"),
                                new StringIdentifier("rare_item_affix_92_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_93"),
                            0,
                            0,
                            new StringIdentifier("item_prefix_name_93"),
                            new StringIdentifier("item_suffix_name_93"),
                            new StringIdentifier("10"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_93_enchantment_0"),
                                new StringIdentifier("magic_item_affix_93_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_93"),
                            0,
                            0,
                            new StringIdentifier("10"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_93_enchantment_0"),
                                new StringIdentifier("rare_item_affix_93_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_94"),
                            0,
                            0,
                            new StringIdentifier("item_prefix_name_94"),
                            new StringIdentifier("item_suffix_name_94"),
                            new StringIdentifier("10"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_94_enchantment_0"),
                                new StringIdentifier("magic_item_affix_94_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_94"),
                            0,
                            0,
                            new StringIdentifier("10"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_94_enchantment_0"),
                                new StringIdentifier("rare_item_affix_94_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_95"),
                            0,
                            0,
                            new StringIdentifier("item_prefix_name_95"),
                            new StringIdentifier("item_suffix_name_95"),
                            new StringIdentifier("10"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_95_enchantment_0"),
                                new StringIdentifier("magic_item_affix_95_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_95"),
                            0,
                            0,
                            new StringIdentifier("10"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_95_enchantment_0"),
                                new StringIdentifier("rare_item_affix_95_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_96"),
                            0,
                            0,
                            new StringIdentifier("10"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_96_enchantment_0"),
                                new StringIdentifier("rare_item_affix_96_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_97"),
                            0,
                            0,
                            new StringIdentifier("10"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_97_enchantment_0"),
                                new StringIdentifier("rare_item_affix_97_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_98"),
                            0,
                            0,
                            new StringIdentifier("10"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_98_enchantment_0"),
                                new StringIdentifier("rare_item_affix_98_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_99"),
                            0,
                            0,
                            new StringIdentifier("item_prefix_name_99"),
                            new StringIdentifier("item_suffix_name_99"),
                            new StringIdentifier("10"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_99_enchantment_0"),
                                new StringIdentifier("magic_item_affix_99_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_99"),
                            0,
                            0,
                            new StringIdentifier("10"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_99_enchantment_0"),
                                new StringIdentifier("rare_item_affix_99_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_100"),
                            0,
                            0,
                            new StringIdentifier("item_prefix_name_100"),
                            new StringIdentifier("item_suffix_name_100"),
                            new StringIdentifier("10"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_100_enchantment_0"),
                                new StringIdentifier("magic_item_affix_100_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_100"),
                            0,
                            0,
                            new StringIdentifier("10"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_100_enchantment_0"),
                                new StringIdentifier("rare_item_affix_100_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_101"),
                            0,
                            0,
                            new StringIdentifier("11"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_101_enchantment_0"),
                                new StringIdentifier("rare_item_affix_101_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_102"),
                            0,
                            0,
                            new StringIdentifier("11"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_102_enchantment_0"),
                                new StringIdentifier("rare_item_affix_102_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_103"),
                            0,
                            0,
                            new StringIdentifier("11"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_103_enchantment_0"),
                                new StringIdentifier("rare_item_affix_103_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_104"),
                            0,
                            0,
                            new StringIdentifier("11"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_104_enchantment_0"),
                                new StringIdentifier("rare_item_affix_104_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_105"),
                            0,
                            0,
                            new StringIdentifier("11"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_105_enchantment_0"),
                                new StringIdentifier("rare_item_affix_105_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_106"),
                            0,
                            0,
                            new StringIdentifier("11"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_106_enchantment_0"),
                                new StringIdentifier("rare_item_affix_106_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_107"),
                            0,
                            0,
                            new StringIdentifier("11"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_107_enchantment_0"),
                                new StringIdentifier("rare_item_affix_107_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_108"),
                            0,
                            0,
                            new StringIdentifier("11"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_108_enchantment_0"),
                                new StringIdentifier("rare_item_affix_108_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_109"),
                            0,
                            0,
                            new StringIdentifier("11"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_109_enchantment_0"),
                                new StringIdentifier("rare_item_affix_109_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_110"),
                            0,
                            0,
                            new StringIdentifier("item_prefix_name_110"),
                            new StringIdentifier("item_suffix_name_110"),
                            new StringIdentifier("11"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_110_enchantment_0"),
                                new StringIdentifier("magic_item_affix_110_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_110"),
                            0,
                            0,
                            new StringIdentifier("11"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_110_enchantment_0"),
                                new StringIdentifier("rare_item_affix_110_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_111"),
                            0,
                            0,
                            new StringIdentifier("item_prefix_name_111"),
                            new StringIdentifier("item_suffix_name_111"),
                            new StringIdentifier("12"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_111_enchantment_0"),
                                new StringIdentifier("magic_item_affix_111_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_111"),
                            0,
                            0,
                            new StringIdentifier("12"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_111_enchantment_0"),
                                new StringIdentifier("rare_item_affix_111_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_112"),
                            0,
                            0,
                            new StringIdentifier("12"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_112_enchantment_0"),
                                new StringIdentifier("rare_item_affix_112_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_113"),
                            0,
                            0,
                            new StringIdentifier("item_prefix_name_113"),
                            new StringIdentifier("item_suffix_name_113"),
                            new StringIdentifier("12"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_113_enchantment_0"),
                                new StringIdentifier("magic_item_affix_113_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_113"),
                            0,
                            0,
                            new StringIdentifier("12"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_113_enchantment_0"),
                                new StringIdentifier("rare_item_affix_113_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_114"),
                            0,
                            0,
                            new StringIdentifier("12"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_114_enchantment_0"),
                                new StringIdentifier("rare_item_affix_114_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_115"),
                            0,
                            0,
                            new StringIdentifier("12"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_115_enchantment_0"),
                                new StringIdentifier("rare_item_affix_115_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_116"),
                            0,
                            0,
                            new StringIdentifier("12"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_116_enchantment_0"),
                                new StringIdentifier("rare_item_affix_116_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_117"),
                            0,
                            0,
                            new StringIdentifier("12"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_117_enchantment_0"),
                                new StringIdentifier("rare_item_affix_117_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_118"),
                            0,
                            0,
                            new StringIdentifier("12"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_118_enchantment_0"),
                                new StringIdentifier("rare_item_affix_118_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_119"),
                            0,
                            0,
                            new StringIdentifier("item_prefix_name_119"),
                            new StringIdentifier("item_suffix_name_119"),
                            new StringIdentifier("12"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_119_enchantment_0"),
                                new StringIdentifier("magic_item_affix_119_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_119"),
                            0,
                            0,
                            new StringIdentifier("12"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_119_enchantment_0"),
                                new StringIdentifier("rare_item_affix_119_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_120"),
                            0,
                            0,
                            new StringIdentifier("item_prefix_name_120"),
                            new StringIdentifier("item_suffix_name_120"),
                            new StringIdentifier("12"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_120_enchantment_0"),
                                new StringIdentifier("magic_item_affix_120_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_120"),
                            0,
                            0,
                            new StringIdentifier("12"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_120_enchantment_0"),
                                new StringIdentifier("rare_item_affix_120_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_121"),
                            0,
                            0,
                            new StringIdentifier("item_prefix_name_121"),
                            new StringIdentifier("item_suffix_name_121"),
                            new StringIdentifier("13"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_121_enchantment_0"),
                                new StringIdentifier("magic_item_affix_121_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_121"),
                            0,
                            0,
                            new StringIdentifier("13"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_121_enchantment_0"),
                                new StringIdentifier("rare_item_affix_121_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_122"),
                            0,
                            0,
                            new StringIdentifier("13"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_122_enchantment_0"),
                                new StringIdentifier("rare_item_affix_122_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_123"),
                            0,
                            0,
                            new StringIdentifier("13"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_123_enchantment_0"),
                                new StringIdentifier("rare_item_affix_123_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_124"),
                            0,
                            0,
                            new StringIdentifier("13"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_124_enchantment_0"),
                                new StringIdentifier("rare_item_affix_124_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_125"),
                            0,
                            0,
                            new StringIdentifier("13"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_125_enchantment_0"),
                                new StringIdentifier("rare_item_affix_125_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_126"),
                            0,
                            0,
                            new StringIdentifier("13"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_126_enchantment_0"),
                                new StringIdentifier("rare_item_affix_126_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_127"),
                            0,
                            0,
                            new StringIdentifier("13"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_127_enchantment_0"),
                                new StringIdentifier("rare_item_affix_127_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_128"),
                            0,
                            0,
                            new StringIdentifier("13"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_128_enchantment_0"),
                                new StringIdentifier("rare_item_affix_128_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_129"),
                            0,
                            0,
                            new StringIdentifier("13"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_129_enchantment_0"),
                                new StringIdentifier("rare_item_affix_129_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_130"),
                            0,
                            0,
                            new StringIdentifier("13"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_130_enchantment_0"),
                                new StringIdentifier("rare_item_affix_130_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_131"),
                            0,
                            0,
                            new StringIdentifier("14"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_131_enchantment_0"),
                                new StringIdentifier("rare_item_affix_131_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_132"),
                            0,
                            0,
                            new StringIdentifier("14"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_132_enchantment_0"),
                                new StringIdentifier("rare_item_affix_132_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_133"),
                            0,
                            0,
                            new StringIdentifier("14"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_133_enchantment_0"),
                                new StringIdentifier("rare_item_affix_133_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_134"),
                            0,
                            0,
                            new StringIdentifier("14"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_134_enchantment_0"),
                                new StringIdentifier("rare_item_affix_134_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_135"),
                            0,
                            0,
                            new StringIdentifier("14"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_135_enchantment_0"),
                                new StringIdentifier("rare_item_affix_135_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_136"),
                            0,
                            0,
                            new StringIdentifier("14"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_136_enchantment_0"),
                                new StringIdentifier("rare_item_affix_136_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_137"),
                            0,
                            0,
                            new StringIdentifier("14"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_137_enchantment_0"),
                                new StringIdentifier("rare_item_affix_137_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_138"),
                            0,
                            0,
                            new StringIdentifier("14"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_138_enchantment_0"),
                                new StringIdentifier("rare_item_affix_138_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_139"),
                            0,
                            0,
                            new StringIdentifier("item_prefix_name_139"),
                            new StringIdentifier("item_suffix_name_139"),
                            new StringIdentifier("14"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_139_enchantment_0"),
                                new StringIdentifier("magic_item_affix_139_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_139"),
                            0,
                            0,
                            new StringIdentifier("14"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_139_enchantment_0"),
                                new StringIdentifier("rare_item_affix_139_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_140"),
                            0,
                            0,
                            new StringIdentifier("item_prefix_name_140"),
                            new StringIdentifier("item_suffix_name_140"),
                            new StringIdentifier("14"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_140_enchantment_0"),
                                new StringIdentifier("magic_item_affix_140_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_140"),
                            0,
                            0,
                            new StringIdentifier("14"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_140_enchantment_0"),
                                new StringIdentifier("rare_item_affix_140_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_141"),
                            0,
                            0,
                            new StringIdentifier("15"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_141_enchantment_0"),
                                new StringIdentifier("rare_item_affix_141_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_142"),
                            0,
                            0,
                            new StringIdentifier("15"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_142_enchantment_0"),
                                new StringIdentifier("rare_item_affix_142_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_143"),
                            0,
                            0,
                            new StringIdentifier("15"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_143_enchantment_0"),
                                new StringIdentifier("rare_item_affix_143_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_144"),
                            0,
                            0,
                            new StringIdentifier("15"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_144_enchantment_0"),
                                new StringIdentifier("rare_item_affix_144_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_145"),
                            0,
                            0,
                            new StringIdentifier("15"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_145_enchantment_0"),
                                new StringIdentifier("rare_item_affix_145_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_146"),
                            0,
                            0,
                            new StringIdentifier("15"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_146_enchantment_0"),
                                new StringIdentifier("rare_item_affix_146_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_147"),
                            0,
                            0,
                            new StringIdentifier("15"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_147_enchantment_0"),
                                new StringIdentifier("rare_item_affix_147_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_148"),
                            0,
                            0,
                            new StringIdentifier("15"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_148_enchantment_0"),
                                new StringIdentifier("rare_item_affix_148_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_149"),
                            0,
                            0,
                            new StringIdentifier("15"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_149_enchantment_0"),
                                new StringIdentifier("rare_item_affix_149_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_150"),
                            0,
                            0,
                            new StringIdentifier("15"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_150_enchantment_0"),
                                new StringIdentifier("rare_item_affix_150_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_151"),
                            0,
                            0,
                            new StringIdentifier("16"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_151_enchantment_0"),
                                new StringIdentifier("rare_item_affix_151_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_152"),
                            0,
                            0,
                            new StringIdentifier("16"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_152_enchantment_0"),
                                new StringIdentifier("rare_item_affix_152_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_153"),
                            0,
                            0,
                            new StringIdentifier("16"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_153_enchantment_0"),
                                new StringIdentifier("rare_item_affix_153_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_154"),
                            0,
                            0,
                            new StringIdentifier("16"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_154_enchantment_0"),
                                new StringIdentifier("rare_item_affix_154_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_155"),
                            0,
                            0,
                            new StringIdentifier("16"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_155_enchantment_0"),
                                new StringIdentifier("rare_item_affix_155_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_156"),
                            0,
                            0,
                            new StringIdentifier("16"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_156_enchantment_0"),
                                new StringIdentifier("rare_item_affix_156_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_157"),
                            0,
                            0,
                            new StringIdentifier("16"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_157_enchantment_0"),
                                new StringIdentifier("rare_item_affix_157_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_158"),
                            0,
                            0,
                            new StringIdentifier("16"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_158_enchantment_0"),
                                new StringIdentifier("rare_item_affix_158_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_159"),
                            0,
                            0,
                            new StringIdentifier("16"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_159_enchantment_0"),
                                new StringIdentifier("rare_item_affix_159_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_160"),
                            0,
                            0,
                            new StringIdentifier("16"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_160_enchantment_0"),
                                new StringIdentifier("rare_item_affix_160_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_161"),
                            0,
                            0,
                            new StringIdentifier("17"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_161_enchantment_0"),
                                new StringIdentifier("rare_item_affix_161_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_162"),
                            0,
                            0,
                            new StringIdentifier("17"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_162_enchantment_0"),
                                new StringIdentifier("rare_item_affix_162_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_163"),
                            0,
                            0,
                            new StringIdentifier("17"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_163_enchantment_0"),
                                new StringIdentifier("rare_item_affix_163_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_164"),
                            0,
                            0,
                            new StringIdentifier("17"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_164_enchantment_0"),
                                new StringIdentifier("rare_item_affix_164_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_165"),
                            0,
                            0,
                            new StringIdentifier("17"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_165_enchantment_0"),
                                new StringIdentifier("rare_item_affix_165_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_166"),
                            0,
                            0,
                            new StringIdentifier("17"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_166_enchantment_0"),
                                new StringIdentifier("rare_item_affix_166_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_167"),
                            0,
                            0,
                            new StringIdentifier("17"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_167_enchantment_0"),
                                new StringIdentifier("rare_item_affix_167_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_168"),
                            0,
                            0,
                            new StringIdentifier("17"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_168_enchantment_0"),
                                new StringIdentifier("rare_item_affix_168_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_169"),
                            0,
                            0,
                            new StringIdentifier("17"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_169_enchantment_0"),
                                new StringIdentifier("rare_item_affix_169_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_170"),
                            0,
                            0,
                            new StringIdentifier("17"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_170_enchantment_0"),
                                new StringIdentifier("rare_item_affix_170_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_171"),
                            0,
                            0,
                            new StringIdentifier("item_prefix_name_171"),
                            new StringIdentifier("item_suffix_name_171"),
                            new StringIdentifier("18"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_171_enchantment_0"),
                                new StringIdentifier("magic_item_affix_171_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_171"),
                            0,
                            0,
                            new StringIdentifier("18"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_171_enchantment_0"),
                                new StringIdentifier("rare_item_affix_171_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_172"),
                            0,
                            0,
                            new StringIdentifier("item_prefix_name_172"),
                            new StringIdentifier("item_suffix_name_172"),
                            new StringIdentifier("18"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_172_enchantment_0"),
                                new StringIdentifier("magic_item_affix_172_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_172"),
                            0,
                            0,
                            new StringIdentifier("18"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_172_enchantment_0"),
                                new StringIdentifier("rare_item_affix_172_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_173"),
                            0,
                            0,
                            new StringIdentifier("item_prefix_name_173"),
                            new StringIdentifier("item_suffix_name_173"),
                            new StringIdentifier("18"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_173_enchantment_0"),
                                new StringIdentifier("magic_item_affix_173_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_173"),
                            0,
                            0,
                            new StringIdentifier("18"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_173_enchantment_0"),
                                new StringIdentifier("rare_item_affix_173_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_174"),
                            0,
                            0,
                            new StringIdentifier("item_prefix_name_174"),
                            new StringIdentifier("item_suffix_name_174"),
                            new StringIdentifier("18"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_174_enchantment_0"),
                                new StringIdentifier("magic_item_affix_174_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_174"),
                            0,
                            0,
                            new StringIdentifier("18"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_174_enchantment_0"),
                                new StringIdentifier("rare_item_affix_174_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_175"),
                            0,
                            0,
                            new StringIdentifier("item_prefix_name_175"),
                            new StringIdentifier("item_suffix_name_175"),
                            new StringIdentifier("18"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_175_enchantment_0"),
                                new StringIdentifier("magic_item_affix_175_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_175"),
                            0,
                            0,
                            new StringIdentifier("18"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_175_enchantment_0"),
                                new StringIdentifier("rare_item_affix_175_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_176"),
                            0,
                            0,
                            new StringIdentifier("item_prefix_name_176"),
                            new StringIdentifier("item_suffix_name_176"),
                            new StringIdentifier("18"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_176_enchantment_0"),
                                new StringIdentifier("magic_item_affix_176_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_176"),
                            0,
                            0,
                            new StringIdentifier("18"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_176_enchantment_0"),
                                new StringIdentifier("rare_item_affix_176_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_177"),
                            0,
                            0,
                            new StringIdentifier("item_prefix_name_177"),
                            new StringIdentifier("item_suffix_name_177"),
                            new StringIdentifier("18"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_177_enchantment_0"),
                                new StringIdentifier("magic_item_affix_177_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_177"),
                            0,
                            0,
                            new StringIdentifier("18"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_177_enchantment_0"),
                                new StringIdentifier("rare_item_affix_177_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_178"),
                            0,
                            0,
                            new StringIdentifier("item_prefix_name_178"),
                            new StringIdentifier("item_suffix_name_178"),
                            new StringIdentifier("18"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_178_enchantment_0"),
                                new StringIdentifier("magic_item_affix_178_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_178"),
                            0,
                            0,
                            new StringIdentifier("18"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_178_enchantment_0"),
                                new StringIdentifier("rare_item_affix_178_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_179"),
                            0,
                            0,
                            new StringIdentifier("item_prefix_name_179"),
                            new StringIdentifier("item_suffix_name_179"),
                            new StringIdentifier("18"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_179_enchantment_0"),
                                new StringIdentifier("magic_item_affix_179_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_179"),
                            0,
                            0,
                            new StringIdentifier("18"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_179_enchantment_0"),
                                new StringIdentifier("rare_item_affix_179_enchantment_1")
                            }),
                        affixTemplate.CreateMagicAffix(
                            new StringIdentifier("magic_item_affix_180"),
                            0,
                            0,
                            new StringIdentifier("item_prefix_name_180"),
                            new StringIdentifier("item_suffix_name_180"),
                            new StringIdentifier("18"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("magic_item_affix_180_enchantment_0"),
                                new StringIdentifier("magic_item_affix_180_enchantment_1")
                            }),
                        affixTemplate.CreateRareAffix(
                            new StringIdentifier("rare_item_affix_180"),
                            0,
                            0,
                            new StringIdentifier("18"),
                            new IIdentifier[]
                            {
                                new StringIdentifier("weapon")
                            },
                            new[]
                            {
                                new StringIdentifier("rare_item_affix_180_enchantment_0"),
                                new StringIdentifier("rare_item_affix_180_enchantment_1")
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