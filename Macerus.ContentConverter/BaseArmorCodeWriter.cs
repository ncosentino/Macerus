using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using ProjectXyz.Plugins.Features.Stats;

namespace Macerus.ContentConverter
{
    public sealed class BaseArmorCodeWriter
    {
        public void WriteBaseArmorCode(
            IEnumerable<BaseArmorDto> baseArmorDtos,
            IReadOnlyStatDefinitionToTermMappingRepository statDefinitionToTermMappingRepository,
            string outputDirectory)
        {
            var templatedItemDefinitionCode = @$"using System;
using System.Collections.Generic;

using Autofac;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Content.Affixes;
using Macerus.Plugins.Features.GameObjects.Items;
using Macerus.Plugins.Features.Stats.Default;
using Macerus.Shared.Behaviors;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Items;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.InMemory;
using ProjectXyz.Shared.Framework;

namespace Macerus.Content.Generated.Items
{{
    public sealed class BaseArmorsModule : SingleRegistrationModule
    {{
        protected override void SafeLoad(ContainerBuilder builder)
        {{
            builder
                .Register(c =>
                {{
                    var filterContextAmenity = c.Resolve<IFilterContextAmenity>();
                    var itemIdentifiers = c.Resolve<IItemIdentifiers>();
                    var itemDefinitions = new[]
                    {{
{string.Join(",\r\n", baseArmorDtos.Select(x => $"						{GetArmorItemDefinitionCodeTemplate(x, statDefinitionToTermMappingRepository)}"))}
                    }};
                    var itemDefinitionRepository = new InMemoryItemDefinitionRepository(
                        c.Resolve<IAttributeFilterer>(),
                        itemDefinitions);
                    return itemDefinitionRepository;
                }})
                .SingleInstance()
                .AsImplementedInterfaces();
        }}
    }}
}}
";
            var directoryPath = Path.Combine(outputDirectory, @"Generated\Items");
            Directory.CreateDirectory(directoryPath);

            var filePath = Path.Combine(directoryPath, "BaseArmorsModule.cs");
            File.Delete(filePath);
            File.WriteAllText(filePath, templatedItemDefinitionCode);
        }

        private string GetArmorItemDefinitionCodeTemplate(
            BaseArmorDto baseArmorDto,
            IReadOnlyStatDefinitionToTermMappingRepository statDefinitionToTermMappingRepository)
        {
            var physicalDamageCode = baseArmorDto.ItemArmorMaximum < 1
                ? string.Empty
                : @$"new RandomStatRangeGeneratorComponent(new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("ARMOR").StatDefinitionId}""), {baseArmorDto.ItemArmorMinimum}, {baseArmorDto.ItemArmorMaximum}, 0), // armor";
            var magicDamageCode = baseArmorDto.ItemEvasionMaximum < 1
               ? string.Empty
               : @$"new RandomStatRangeGeneratorComponent(new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("EVASION").StatDefinitionId}""), {baseArmorDto.ItemEvasionMinimum}, {baseArmorDto.ItemEvasionMaximum}, 0), // evasion";

            var itemDefinitionCodeTemplate = @$"
                        new ItemDefinition(
                            new[]
                            {{
                                AffixFilterAttributes.AllowsNormalMagicAndRareAffix,
                                filterContextAmenity.CreateSupportedAttribute(
                                    itemIdentifiers.ItemDefinitionIdentifier,
                                    new StringIdentifier(""{baseArmorDto.ItemId}"")),
                            }},
                            new IGeneratorComponent[]
                            {{
                                new NameGeneratorComponent(""{baseArmorDto.ItemNameStringResourceId}""),
                                new IconGeneratorComponent(""{baseArmorDto.ItemIconResourceId}""),
                                new EquippableGeneratorComponent(new[] {{ new StringIdentifier(""{baseArmorDto.ItemEquipSlotId}"") }}),
                                {(
                                baseArmorDto.ItemSocketsMaximum > 0
                                    ? @$"new SocketGeneratorComponent(new[]
                                            {{
                                                KeyValuePair.Create((IIdentifier)new StringIdentifier(""socket_type_gem""), Tuple.Create({baseArmorDto.ItemSocketsMinimum}, {baseArmorDto.ItemSocketsMaximum})),
                                                KeyValuePair.Create((IIdentifier)new StringIdentifier(""socket_type_jewel""), Tuple.Create({baseArmorDto.ItemSocketsMinimum}, {baseArmorDto.ItemSocketsMaximum})),
                                                KeyValuePair.Create((IIdentifier)new StringIdentifier(""socket_type_rune""), Tuple.Create({baseArmorDto.ItemSocketsMinimum}, {baseArmorDto.ItemSocketsMaximum})),
                                            }},
                                            {baseArmorDto.ItemSocketsMaximum}),".Trim()
                                    : string.Empty
                                )}
                                new ItemTagsGeneratorComponent(new IIdentifier[]
                                {{
{string.Join(",\r\n", baseArmorDto.Tags.Select(x => @$"                                    new StringIdentifier(""{x}"")"))}
                                }}),
                                new HasStatsGeneratorComponent(new Dictionary<IIdentifier, double>()
                                {{
                                    [new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("ITEM_LEVEL").StatDefinitionId}"")] = {baseArmorDto.ItemLevel}, // item level
                                    [new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("BLOCK").StatDefinitionId}"")] = {baseArmorDto.ItemBlock}, // block
{GetRequirementsCode(baseArmorDto, statDefinitionToTermMappingRepository)}
                                    // durability
                                    [new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("DURABILITY_MAXIMUM").StatDefinitionId}"")] = {(baseArmorDto.ItemDurabilityMaximum < 1 ? 0 : baseArmorDto.ItemDurabilityMaximum)}, // durability maximum
                                    [new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("DURABILITY_CURRENT").StatDefinitionId}"")] = {(baseArmorDto.ItemDurabilityMaximum < 1 ? 0 : baseArmorDto.ItemDurabilityMinimum)}, // durability current
                                }}),
                                {physicalDamageCode}
                                {magicDamageCode}
                            }})";
            return itemDefinitionCodeTemplate;
        }

        private string GetRequirementsCode(
            BaseArmorDto baseArmorDto,
            IReadOnlyStatDefinitionToTermMappingRepository statDefinitionToTermMappingRepository)
        {
            var hasRequirements = false;
            var builder = new StringBuilder("                                    // requirements\r\n");
            if (baseArmorDto.ItemLevelRequirement > 0)
            {
                builder.AppendLine($@"                                    [new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("REQUIRED_LEVEL").StatDefinitionId}"")] = {baseArmorDto.ItemLevelRequirement},");
                hasRequirements = true;
            }

            if (baseArmorDto.ItemStrengthRequirement > 0)
            {
                builder.AppendLine($@"                                    [new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("REQUIRED_STRENGTH").StatDefinitionId}"")] = {baseArmorDto.ItemStrengthRequirement},");
                hasRequirements = true;
            }

            if (baseArmorDto.ItemDexterityRequirement > 0)
            {
                builder.AppendLine($@"                                    [new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("REQUIRED_DEXTERITY").StatDefinitionId}"")] = {baseArmorDto.ItemDexterityRequirement},");
                hasRequirements = true;
            }

            if (baseArmorDto.ItemIntelligenceRequirement > 0)
            {
                builder.AppendLine($@"                                    [new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("REQUIRED_INTELLIGENCE").StatDefinitionId}"")] = {baseArmorDto.ItemIntelligenceRequirement},");
                hasRequirements = true;
            }

            if (baseArmorDto.ItemSpeedRequirement > 0)
            {
                builder.AppendLine($@"                                    [new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("REQUIRED_SPEED").StatDefinitionId}"")] = {baseArmorDto.ItemSpeedRequirement},");
                hasRequirements = true;
            }

            if (baseArmorDto.ItemVitalityRequirement > 0)
            {
                builder.AppendLine($@"                                    [new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("REQUIRED_VITALITY").StatDefinitionId}"")] = {baseArmorDto.ItemVitalityRequirement},");
                hasRequirements = true;
            }

            if (!hasRequirements)
            {
                builder.AppendLine("                                    // none!");
            }

            return builder.ToString();
        }
    }
}
