using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using ProjectXyz.Plugins.Features.Stats;

namespace Macerus.ContentConverter
{
    public sealed class BaseWeaponCodeWriter
    {
        public void WriteBaseWeaponCode(
            IEnumerable<BaseWeaponDto> baseWeaponDtos,
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
    public sealed class BaseWeaponsModule : SingleRegistrationModule
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
{string.Join(",\r\n", baseWeaponDtos.Select(x => $"						{GetWeaponItemDefinitionCodeTemplate(x, statDefinitionToTermMappingRepository)}"))}
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

            var filePath = Path.Combine(directoryPath, "BaseWeaponsModule.cs");
            File.Delete(filePath);
            File.WriteAllText(filePath, templatedItemDefinitionCode);
        }

        private string GetWeaponItemDefinitionCodeTemplate(
            BaseWeaponDto baseWeaponDto,
            IReadOnlyStatDefinitionToTermMappingRepository statDefinitionToTermMappingRepository)
        {
            var physicalDamageCode = baseWeaponDto.ItemPhysicalDamageMaxMax < 1
                     ? string.Empty
                     : @$"new RandomStatRangeGeneratorComponent(new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("PHYSICAL_DAMAGE_MIN").StatDefinitionId}""), {baseWeaponDto.ItemPhysicalDamageMinMin}, {baseWeaponDto.ItemPhysicalDamageMinMax}, 0),
                                new RandomStatRangeGeneratorComponent(new StringIdentifier(""{ statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("PHYSICAL_DAMAGE_MAX").StatDefinitionId }""), { baseWeaponDto.ItemPhysicalDamageMaxMin}, { baseWeaponDto.ItemPhysicalDamageMaxMax}, 0),";
            var magicDamageCode = baseWeaponDto.ItemMagicDamageMaxMax < 1
               ? string.Empty
               : @$"new RandomStatRangeGeneratorComponent(new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("MAGIC_DAMAGE_MIN").StatDefinitionId}""), {baseWeaponDto.ItemMagicDamageMinMin}, {baseWeaponDto.ItemMagicDamageMinMax}, 0),
                                new RandomStatRangeGeneratorComponent(new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("MAGIC_DAMAGE_MAX").StatDefinitionId}""), {baseWeaponDto.ItemMagicDamageMaxMin}, {baseWeaponDto.ItemMagicDamageMaxMax}, 0),";

            var itemDefinitionCodeTemplate = @$"
                        new ItemDefinition(
                            new[]
                            {{
                                AffixFilterAttributes.RequiresNormalAffix,
                                filterContextAmenity.CreateSupportedAttribute(
                                    itemIdentifiers.ItemDefinitionIdentifier,
                                    new StringIdentifier(""{baseWeaponDto.ItemId}"")),
                            }},
                            new IGeneratorComponent[]
                            {{
                                new NameGeneratorComponent(""{baseWeaponDto.ItemNameStringResourceId}""),
                                new IconGeneratorComponent(""{baseWeaponDto.ItemIconResourceId}""),
                                new EquippableGeneratorComponent(new[] {{ new StringIdentifier(""{baseWeaponDto.ItemEquipSlotId}"") }}),
                                {(
                                baseWeaponDto.ItemSocketsMaximum > 0
                                    ? @$"new SocketGeneratorComponent(new[]
                                            {{
                                                KeyValuePair.Create((IIdentifier)new StringIdentifier(""socket_type_gem""), Tuple.Create({baseWeaponDto.ItemSocketsMinimum}, {baseWeaponDto.ItemSocketsMaximum})),
                                                KeyValuePair.Create((IIdentifier)new StringIdentifier(""socket_type_jewel""), Tuple.Create({baseWeaponDto.ItemSocketsMinimum}, {baseWeaponDto.ItemSocketsMaximum})),
                                                KeyValuePair.Create((IIdentifier)new StringIdentifier(""socket_type_rune""), Tuple.Create({baseWeaponDto.ItemSocketsMinimum}, {baseWeaponDto.ItemSocketsMaximum})),
                                            }},
                                            {baseWeaponDto.ItemSocketsMaximum}),".Trim()
                                    : string.Empty
                            )}
                                new HasStatsGeneratorComponent(new Dictionary<IIdentifier, double>()
                                {{
                                    [new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("ITEM_LEVEL").StatDefinitionId}"")] = {baseWeaponDto.ItemLevel},
                                    [new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("ATTACK_SPEED").StatDefinitionId}"")] = {baseWeaponDto.ItemAttackSpeed},
                                    [new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("RANGE").StatDefinitionId}"")] = {baseWeaponDto.ItemRange},
{GetRequirementsCode(baseWeaponDto, statDefinitionToTermMappingRepository)}
                                    // durability
                                    [new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("DURABILITY_MAXIMUM").StatDefinitionId}"")] = {(baseWeaponDto.ItemDurabilityMaximum < 1 ? 0 : baseWeaponDto.ItemDurabilityMaximum)},
                                    [new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("DURABILITY_CURRENT").StatDefinitionId}"")] = {(baseWeaponDto.ItemDurabilityMaximum < 1 ? 0 : baseWeaponDto.ItemDurabilityMinimum)},
                                }}),
                                {physicalDamageCode}
                                {magicDamageCode}
                            }})";
            return itemDefinitionCodeTemplate;
        }

        private string GetRequirementsCode(
            BaseWeaponDto baseWeaponDto,
            IReadOnlyStatDefinitionToTermMappingRepository statDefinitionToTermMappingRepository)
        {
            var hasRequirements = false;
            var builder = new StringBuilder("                                    // requirements\r\n");
            if (baseWeaponDto.ItemLevelRequirement > 0)
            {
                builder.AppendLine($@"                                    [new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("REQUIRED_LEVEL").StatDefinitionId}"")] = {baseWeaponDto.ItemLevelRequirement},");
                hasRequirements = true;
            }

            if (baseWeaponDto.ItemStrengthRequirement > 0)
            {
                builder.AppendLine($@"                                    [new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("REQUIRED_STRENGTH").StatDefinitionId}"")] = {baseWeaponDto.ItemStrengthRequirement},");
                hasRequirements = true;
            }

            if (baseWeaponDto.ItemDexterityRequirement > 0)
            {
                builder.AppendLine($@"                                    [new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("REQUIRED_DEXTERITY").StatDefinitionId}"")] = {baseWeaponDto.ItemDexterityRequirement},");
                hasRequirements = true;
            }

            if (baseWeaponDto.ItemIntelligenceRequirement > 0)
            {
                builder.AppendLine($@"                                    [new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("REQUIRED_INTELLIGENCE").StatDefinitionId}"")] = {baseWeaponDto.ItemIntelligenceRequirement},");
                hasRequirements = true;
            }

            if (baseWeaponDto.ItemSpeedRequirement > 0)
            {
                builder.AppendLine($@"                                    [new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("REQUIRED_SPEED").StatDefinitionId}"")] = {baseWeaponDto.ItemSpeedRequirement},");
                hasRequirements = true;
            }

            if (baseWeaponDto.ItemVitalityRequirement > 0)
            {
                builder.AppendLine($@"                                    [new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("REQUIRED_VITALITY").StatDefinitionId}"")] = {baseWeaponDto.ItemVitalityRequirement},");
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
