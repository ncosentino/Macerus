using System;
using System.Collections.Generic;

using Autofac;

using Macerus.Plugins.Features.GameObjects.Items.Generation.Rare;

using ProjectXyz.Api.Framework;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.CommonBehaviors.Filtering;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes; // FIXME: dependency on non-API
using ProjectXyz.Shared.Framework;

namespace Macerus.Content.Generated.Items
{
    public sealed class RareItemAffixesModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c =>
                {
                    var affixes = new Lazy<IEnumerable<IRareItemAffix>>(() => new IRareItemAffix[]
                    {
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_1"), // Armageddon
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_2"), // Beast
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_3"), // Bitter
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_4"), // Blackhorn
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_5"), // Blood
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_6"), // Bone
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_7"), // Bramble
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_8"), // Brimstone
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_9"), // Carrion
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_10"), // Chaos
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_11"), // Corpse
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_12"), // Corruption
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_13"), // Cruel
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_14"), // Dire
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_15"), // Death
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_16"), // Demon
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_17"), // Doom
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_18"), // Dread
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_19"), // Eagle
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_20"), // Entropy
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_21"), // Fiend
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_22"), // Gale
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_23"), // Ghoul
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_24"), // Glyph
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_25"), // Grim
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_26"), // Hailstone
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_27"), // Havoc
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_28"), // Imp
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_29"), // Loath
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_30"), // Order
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_31"), // Pain
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_32"), // Plague
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_33"), // Raven
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_34"), // Rule
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_35"), // Rune
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_36"), // Shadow
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_37"), // Skull
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_38"), // Stone
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_39"), // Storm
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_40"), // Soul
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_41"), // Spirit
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_42"), // Viper
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_43"), // Wraith
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_44"), // Aegis
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("shield"),
                                        new StringIdentifier("jewellery")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_45"), // Badge
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("shield"),
                                        new StringIdentifier("jewellery")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_46"), // Band
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("ring")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_47"), // Bar
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_48"), // Barb
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("weapon")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_49"), // Beads
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("ring"),
                                        new StringIdentifier("amulet")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_50"), // Bite
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("weapon")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_51"), // Blazer
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("weapon"),
                                        new StringIdentifier("feet")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_52"), // Blow
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("weapon")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_53"), // Bludgeon
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("blunt")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_54"), // Bolt
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("bow"),
                                        new StringIdentifier("xbow")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_55"), // Branch
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("staff"),
                                        new StringIdentifier("wand")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_56"), // Brand
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("weapon")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_57"), // Breaker
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("weapon")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_58"), // Brogues
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("feet")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_59"), // Brow
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("head")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_60"), // Buckle
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("belt")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_61"), // Carapace
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("body")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_62"), // Casque
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("head")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_63"), // Circle
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_64"), // Circlet
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("head")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_65"), // Chain
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_66"), // Clasp
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("amulet")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_67"), // Claw
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_68"), // Cleaver
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_69"), // Cloak
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_70"), // Clutches
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("hands")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_71"), // Coat
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("body"),
                                        new StringIdentifier("back")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_72"), // Coil
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("belt")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_73"), // Collar
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("amulet")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_74"), // Cord
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("belt")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_75"), // Cowl
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("head")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_76"), // Crack
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("weapon")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_77"), // Crest
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("head")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_78"), // Crusher
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("weapon")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_79"), // Cry
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_80"), // Dart
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("weapon")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_81"), // Edge
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("blade")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_82"), // Emblem
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_83"), // Eye
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_84"), // Fang
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("weapon")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_85"), // Flange
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("weapon")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_86"), // Fletch
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("bow"),
                                        new StringIdentifier("xbow")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_87"), // Flight
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("bow"),
                                        new StringIdentifier("xbow")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_88"), // Finger
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("hands"),
                                        new StringIdentifier("ring")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_89"), // Fist
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("hands")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_90"), // Fringe
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_91"), // Gnarl
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_92"), // Gnash
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("weapon")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_93"), // Goad
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("amulet")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_94"), // Gorget
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_95"), // Grasp
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("hands"),
                                        new StringIdentifier("ring")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_96"), // Greaves
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("feet")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_97"), // Grinder
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("weapon")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_98"), // Grip
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("hands")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_99"), // Guard
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("armor"),
                                        new StringIdentifier("jewellery")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_100"), // Gutter
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("weapon")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_101"), // Gyre
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_102"), // Hand
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("weapon"),
                                        new StringIdentifier("hands")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_103"), // Harness
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_104"), // Harp
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("bow"),
                                        new StringIdentifier("xbow")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_105"), // Hide
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("body")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_106"), // Heart
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_107"), // Hew
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("weapon")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_108"), // Hold
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("hands"),
                                        new StringIdentifier("jewellery")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_109"), // Hood
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("head")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_110"), // Horn
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("weapon"),
                                        new StringIdentifier("head")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_111"), // Impaler
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("weapon")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_112"), // Jack
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("body"),
                                        new StringIdentifier("back")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_113"), // Knell
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("weapon")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_114"), // Knot
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("belt"),
                                        new StringIdentifier("ring")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_115"), // Knuckle
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("gloves"),
                                        new StringIdentifier("ring")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_116"), // Lance
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("blade"),
                                        new StringIdentifier("spear")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_117"), // Lash
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("weapon")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_118"), // Lock
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_119"), // Loom
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("bow"),
                                        new StringIdentifier("xbow")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_120"), // Loop
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("jewellery")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_121"), // Mallet
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("blunt")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_122"), // Mangler
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("weapon")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_123"), // Mantle
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("body"),
                                        new StringIdentifier("back")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_124"), // Mar
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("weapon")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_125"), // Mark
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_126"), // Mask
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("head")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_127"), // Master
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_128"), // Nails
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("hands")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_129"), // Needle
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("weapon")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_130"), // Nock
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("bow"),
                                        new StringIdentifier("xbow")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_131"), // Noose
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("amulet")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_132"), // Pale
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("shield")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_133"), // Pelt
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("body"),
                                        new StringIdentifier("back")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_134"), // Picket
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("axe")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_135"), // Prod
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("spear")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_136"), // Quarrel
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("bow"),
                                        new StringIdentifier("xbow")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_137"), // Quill
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("bow"),
                                        new StringIdentifier("xbow"),
                                        new StringIdentifier("spear"),
                                        new StringIdentifier("sword")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_138"), // Razor
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("sword")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_139"), // Reaver
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("weapon")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_140"), // Rend
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("weapon")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_141"), // Rock
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("jewellery"),
                                        new StringIdentifier("shield")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_142"), // Saw
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("sword"),
                                        new StringIdentifier("axe")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_143"), // Scalpel
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("sword")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_144"), // Scarab
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("jewellery")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_145"), // Scourge
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("weapon")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_146"), // Scratch
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("blade"),
                                        new StringIdentifier("spear")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_147"), // Scythe
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("axe"),
                                        new StringIdentifier("sword")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_148"), // Sever
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("weapon")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_149"), // Shank
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("dagger"),
                                        new StringIdentifier("sword")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_150"), // Shell
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("body"),
                                        new StringIdentifier("back"),
                                        new StringIdentifier("shield")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_151"), // Cap
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("head")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_152"), // Shield
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("armor")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_153"), // Shroud
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("body"),
                                        new StringIdentifier("back")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_154"), // Skewer
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("blade"),
                                        new StringIdentifier("spear")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_155"), // Slayer
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("weapon")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_156"), // Smasher
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("weapon")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_157"), // Song
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_158"), // Spawn
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_159"), // Spike
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("weapon")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_160"), // Spiral
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("jewellery")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_161"), // Splitter
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("weapon")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_162"), // Spur
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("feet")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_163"), // Stake
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("blade"),
                                        new StringIdentifier("spear")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_164"), // Stalker
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("weapon"),
                                        new StringIdentifier("feet")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_165"), // Star
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_166"), // Stinger
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("weapon")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_167"), // Strap
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("belt")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_168"), // Suit
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("body")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_169"), // Sunder
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("weapon")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_170"), // Talisman
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("amulet")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_171"), // Thirst
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_172"), // Tooth
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("blade")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_173"), // Torc
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("amulet")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_174"), // Touch
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("weapon"),
                                        new StringIdentifier("gloves")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_175"), // Tower
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("shield")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_176"), // Track
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("feet")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_177"), // Trample
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("feet")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_178"), // Tread
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("feet")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_179"), // Turn
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_180"), // Veil
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("head")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_181"), // Visage
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("head")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_182"), // Visor
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("head")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_183"), // Wand
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("wand")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_184"), // Ward
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("shield"),
                                        new StringIdentifier("jewellery")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_185"), // Weaver
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("magic"),
                                        new StringIdentifier("jewellery")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_186"), // Winding
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("belt"),
                                        new StringIdentifier("jewellery")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_187"), // Wing
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("shield")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_188"), // Whorl
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("amulet")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_189"), // Wood
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("bow"),
                                        new StringIdentifier("xbow")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_190"), // Wrack
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("bow"),
                                        new StringIdentifier("xbow")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_191"), // Wrap
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("belt")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_192"), // Wretched
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_193"), // Alpha
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_194"), // Mystery
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_195"), // Primal
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_196"), // Eternity
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_197"), // Shadow
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_198"), // Phantom
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_199"), // Ghost
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_200"), // Spirit
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_201"), // Terror
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_202"), // Nether
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_203"), // Mirage
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_204"), // Mirage
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_205"), // Storm
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_206"), // Soul
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_207"), // Shard
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("weapon"),
                                        new StringIdentifier("jewellery")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_208"), // Keeper
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_209"), // Insanity
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_210"), // Rage
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_211"), // Rage
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_212"), // Kiss
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_213"), // Night
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_214"), // Day
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_215"), // Reaper
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_216"), // Reaper
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("weapon")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_217"), // Arcane
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_218"), // Truth
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_219"), // Fall
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_220"), // Light
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_221"), // Ravager
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("weapon")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_222"), // Secret
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_223"), // Secret
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_224"), // Star
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_225"), // Infinity
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_226"), // Moon
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_227"), // Sun
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_228"), // Cage
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("body"),
                                        new StringIdentifier("head")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_229"), // Torment
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_230"), // Torment
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_231"), // Malice
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_232"), // Malice
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_233"), // Omen
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_234"), // Curse
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_235"), // Curse
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_236"), // Hate
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_237"), // Sorrow
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_238"), // Sorrow
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_239"), // Glory
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_240"), // Glory
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_241"), // Oath
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_242"), // Oath
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_243"), // Dusk
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_244"), // Dawn
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_245"), // Prophecy
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_246"), // Prophecy
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_247"), // Ash
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_248"), // Greed
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_249"), // Eater
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_250"), // Faith
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_251"), // Faith
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_252"), // Foul
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_253"), // Echo
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_254"), // Echo
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_255"), // Horror
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_256"), // Whisper
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_257"), // Whisper
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_258"), // Vessel
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("body"),
                                        new StringIdentifier("weapon"),
                                        new StringIdentifier("jewellery")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_259"), // Torrent
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("magic"),
                                        new StringIdentifier("ranged")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_260"), // Flesh
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_261"), // Twist
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("jewellery")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_262"), // Sky
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_263"), // Promise
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_264"), // Bauble
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("jewellery")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_265"), // Aspect
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_266"), // Warp
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_267"), // Warp
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_268"), // Scorch
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_269"), // Midnight
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_270"), // Tranquil
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_271"), // Thunder
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_272"), // Lust
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_273"), // Lust
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_274"), // Nemesis
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_275"), // Nemesis
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_276"), // Fury
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_277"), // Fury
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_278"), // Bond
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_279"), // Fool
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_280"), // Spire
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),
                                new FilterAttribute(
                                    new StringIdentifier("tags"),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {
                                        new StringIdentifier("magic"),
                                        new StringIdentifier("head")
                                    }),
                                    true),
                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_281"), // Twilight
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_282"), // Stone
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_283"), // Mist
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_284"), // Bringer
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_285"), // Ender
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_286"), // Riddle
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_287"), // Riddle
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_288"), // Betrayer
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_289"), // Requiem
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_290"), // Dirge
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_291"), // Chant
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_292"), // Widow
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_293"), // Widow
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_294"), // Jaw
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_295"), // Devourer
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_296"), // Desire
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_297"), // Desire
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_298"), // Comet
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_299"), // Meteor
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_300"), // Amnesia
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_301"), // Nightmare
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_302"), // Eternal
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_303"), // Abyss
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_304"), // Abyss
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_305"), // Purity
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_306"), // Woe
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(true),
                                    true),

                            }),
                        new RareItemAffix(
                            new StringIdentifier("rare_item_affix_307"), // Scream
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("is-prefix"),
                                    new BooleanFilterAttributeValue(false),
                                    true),

                            })
                    });
                    var repository = new InMemoryRareAffixRepository(
                        c.Resolve<Lazy<IAttributeFilterer>>(),
                        affixes);
                    return repository;
                })
                .SingleInstance()
                .AsImplementedInterfaces();
        }
    }
}