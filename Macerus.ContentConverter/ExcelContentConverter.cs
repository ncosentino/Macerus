using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Stats;
using ProjectXyz.Plugins.Features.Stats.Default;
using ProjectXyz.Shared.Framework;

namespace Macerus.ContentConverter
{
    public sealed class ExcelContentConverter
    {
        private readonly ISheetHelper _sheetHelper;

        public ExcelContentConverter(ISheetHelper sheetHelper)
        {
            _sheetHelper = sheetHelper;
        }

        public void Convert(
            string gameDataUrl,
            string outputDirectory)
        {
            var gameDataSourceLocalFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Macerus Game Data.xlsx");

            Console.WriteLine($"Fetching game data from '{gameDataUrl}'...");
            using (var webClient = new WebClient())
            {
                File.WriteAllBytes(gameDataSourceLocalFilePath, webClient.DownloadData(gameDataUrl));
            }

            Console.WriteLine($"Game data written to '{gameDataSourceLocalFilePath}'.");

            outputDirectory = new DirectoryInfo(outputDirectory).FullName;

            Console.WriteLine($"Converting data with output directory '{outputDirectory}'...");

            var stringResourceContentConverter = new StringResourceCodeWriter();

            var affixConverter = new AffixesExcelContentConverter(_sheetHelper);
            var affixCodeWriter = new AffixCodeWriter();

            var uniqueItemConverer = new UniqueItemExcelContentConverter(_sheetHelper);
            var uniqueItemCodeWriter = new UniqueItemCodeWriter();

            var enchantmentDefinitionCodeWriter = new EnchantmentDefinitionCodeWriter();

            IEnumerable<StringResourceDto> stringResourceDtos = new List<StringResourceDto>();
            IEnumerable<EnchantmentDefinitionDto> enchantmentDefinitionDtos = new List<EnchantmentDefinitionDto>();
            using (var filestream = File.Open(gameDataSourceLocalFilePath, FileMode.Open, FileAccess.Read))
            {
                var workbook = new XSSFWorkbook(filestream);
                var statDefinitionToTermMappingRepository = ConvertStats(workbook);

                var affixContent = affixConverter.GetAffixContent(
                    workbook,
                    statDefinitionToTermMappingRepository);
                affixCodeWriter.WriteAffixesCode(
                    affixContent.SelectMany(x => x.AffixDtos),
                    outputDirectory);
                enchantmentDefinitionDtos = enchantmentDefinitionDtos.Concat(affixContent.SelectMany(x => x.EnchantmentDefinitionDtos));
                stringResourceDtos = stringResourceDtos.Concat(affixContent.SelectMany(x => x.StringResourceDtos));

                ConvertBaseArmor(workbook, statDefinitionToTermMappingRepository);
                ConvertBaseWeapons(workbook, statDefinitionToTermMappingRepository);
                
                var uniqueItemContent = uniqueItemConverer.GetUniqueItemContent(
                    workbook, 
                    statDefinitionToTermMappingRepository);
                uniqueItemCodeWriter.WriteUniqueItemsCode(
                    uniqueItemContent.Select(x => x.UniqueItemDto),
                    outputDirectory);
                enchantmentDefinitionDtos = enchantmentDefinitionDtos.Concat(uniqueItemContent.SelectMany(x => x.EnchantmentDefinitionDtos));
                stringResourceDtos = stringResourceDtos.Concat(uniqueItemContent.Select(x => x.StringResourceDto));

                enchantmentDefinitionCodeWriter.WriteEnchantmentDefinitionsCode(
                    enchantmentDefinitionDtos,
                    outputDirectory);

                // at the end after we've accumulated alllllll the resources
                // (which should be optimized to be streamed out and not just
                // loaded into memory like a total dumpster fire)
                stringResourceContentConverter.WriteStringResourceModule(
                    stringResourceDtos,
                    outputDirectory);
            }

            Console.WriteLine("Data has been converted.");
        }

        private void ConvertBaseWeapons(
            XSSFWorkbook workbook,
            IReadOnlyStatDefinitionToTermMappingRepository statDefinitionToTermMappingRepository)
        {
            var baseWeaponsSheet = workbook.GetSheet("Base Weapons");
            var columnHeaderMapping = _sheetHelper.GetColumnHeaderMapping(baseWeaponsSheet.GetRow(0));

            var itemDefinitionCodeTemplates = GetWeaponItemDefinitionCodeTemplates(
                baseWeaponsSheet,
                columnHeaderMapping,
                statDefinitionToTermMappingRepository);

            var templatedItemDefinitionCode = @$"
using System;
using System.Collections.Generic;

using Autofac;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Content.Affixes;
using Macerus.Plugins.Features.GameObjects.Items;
using Macerus.Plugins.Features.GameObjects.Items.Socketing;
using Macerus.Plugins.Features.Inventory.Default.HoverCards;
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
                    var itemDefinitions = new[]
                    {{
{string.Join(",\r\n", itemDefinitionCodeTemplates.Select(x => "						" + x))}
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
            var directoryPath = @"Generated\Items";
            Directory.CreateDirectory(directoryPath);

            var filePath = Path.Combine(directoryPath, "BaseWeaponsModule.cs");
            File.Delete(filePath);
            File.WriteAllText(filePath, templatedItemDefinitionCode);
        }

        private IEnumerable<string> GetWeaponItemDefinitionCodeTemplates(
            ISheet baseArmorSheet,
            IReadOnlyDictionary<string, int> columnHeaderMapping,
            IReadOnlyStatDefinitionToTermMappingRepository statDefinitionToTermMappingRepository)
        {
            for (int rowIndex = 1; rowIndex < baseArmorSheet.PhysicalNumberOfRows; rowIndex++)
            {
                var row = baseArmorSheet.GetRow(rowIndex);
                var itemId = $"weapon_{rowIndex}";

                var itemNameStringResource = row.GetCell(columnHeaderMapping["name"]).StringCellValue;
                var itemNameStringResourceId = $"weapon_name_{rowIndex}";

                var itemIconResource = row.GetCell(columnHeaderMapping["icon"]).StringCellValue;
                var itemIconResourceId = $"weapon_icon_{rowIndex}";

                var itemEquipSlotName = row.GetCell(columnHeaderMapping["slot"]).StringCellValue; // FIXME: Split for multi-slot?
                var itemEquipSlotId = itemEquipSlotName; // FIXME: convert???

                var itemLevel = _sheetHelper.GetIntValue(row, columnHeaderMapping["item level"]);

                var itemAttackSpeed = _sheetHelper.GetIntValue(row, columnHeaderMapping["attack speed"]);
                var itemRange = _sheetHelper.GetIntValue(row, columnHeaderMapping["range"]);

                var itemLevelRequirement = _sheetHelper.GetIntValue(row, columnHeaderMapping["level requirement"]);
                var itemStrengthRequirement = _sheetHelper.GetIntValue(row, columnHeaderMapping["req str"]);
                var itemDexterityRequirement = _sheetHelper.GetIntValue(row, columnHeaderMapping["req dex"]);
                var itemIntelligenceRequirement = _sheetHelper.GetIntValue(row, columnHeaderMapping["req int"]);
                var itemSpeedRequirement = _sheetHelper.GetIntValue(row, columnHeaderMapping["req spd"]);
                var itemVitalityRequirement = _sheetHelper.GetIntValue(row, columnHeaderMapping["req vit"]);

                var itemPhysicalDamageMinMin = _sheetHelper.GetIntValue(row, columnHeaderMapping["physical damage min min"]);
                var itemPhysicalDamageMinMax = _sheetHelper.GetIntValue(row, columnHeaderMapping["physical damage min max"]);
                var itemPhysicalDamageMaxMin = _sheetHelper.GetIntValue(row, columnHeaderMapping["physical damage max min"]);
                var itemPhysicalDamageMaxMax = _sheetHelper.GetIntValue(row, columnHeaderMapping["physical damage max max"]);
                var itemMagicDamageMinMin = _sheetHelper.GetIntValue(row, columnHeaderMapping["magic damage min min"]);
                var itemMagicDamageMinMax = _sheetHelper.GetIntValue(row, columnHeaderMapping["magic damage min max"]);
                var itemMagicDamageMaxMin = _sheetHelper.GetIntValue(row, columnHeaderMapping["magic damage max min"]);
                var itemMagicDamageMaxMax = _sheetHelper.GetIntValue(row, columnHeaderMapping["magic damage max max"]);

                var itemDurabilityMinimum = _sheetHelper.GetIntValue(row, columnHeaderMapping["durability min"]);
                var itemDurabilityMaximum = _sheetHelper.GetIntValue(row, columnHeaderMapping["durability max"]);

                var itemSocketsMinimum = _sheetHelper.GetIntValue(row, columnHeaderMapping["sockets min"]);
                var itemSocketsMaximum = _sheetHelper.GetIntValue(row, columnHeaderMapping["sockets max"]);

                var physicalDamageCode = itemPhysicalDamageMaxMax < 1
                    ? string.Empty
                    : @$"new RandomStatRangeGeneratorComponent({statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("PHYSICAL_DAMAGE_MIN").StatDefinitionId}, {itemPhysicalDamageMinMin}, {itemPhysicalDamageMinMax}, 0),
                                new RandomStatRangeGeneratorComponent({ statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("PHYSICAL_DAMAGE_MAX").StatDefinitionId }, { itemPhysicalDamageMaxMin}, { itemPhysicalDamageMaxMax}, 0),";
                var magicDamageCode = itemMagicDamageMaxMax < 1
                   ? string.Empty
                   : @$"new RandomStatRangeGeneratorComponent({statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("MAGIC_DAMAGE_MIN").StatDefinitionId}, {itemMagicDamageMinMin}, {itemMagicDamageMinMax}, 0),
                                new RandomStatRangeGeneratorComponent({statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("MAGIC_DAMAGE_MAX").StatDefinitionId}, {itemMagicDamageMaxMin}, {itemMagicDamageMaxMax}, 0),";

                var itemDefinitionCodeTemplate = @$"
                        new ItemDefinition(
                            new[]
                            {{
                                AffixFilterAttributes.RequiresNormalAffix,
                                filterContextAmenity.CreateSupportedAttribute(
                                    itemIdentifiers.ItemDefinitionIdentifier,
                                    new StringIdentifier(""{itemId}"")),
                            }},
                            new IGeneratorComponent[]
                            {{
                                new NameGeneratorComponent(""{itemNameStringResourceId}""),
                                new IconGeneratorComponent(""{itemIconResourceId}""),
                                new EquippableGeneratorComponent(new[] {{ new StringIdentifier(""{itemEquipSlotId}"") }}),
                                {(
                                    itemSocketsMaximum > 0
                                        ? @$"new SocketGeneratorComponent(new[]
                                            {{
                                                KeyValuePair.Create((IIdentifier)new StringIdentifier(""socket_type_gem""), Tuple.Create({itemSocketsMinimum}, {itemSocketsMaximum})),
                                                KeyValuePair.Create((IIdentifier)new StringIdentifier(""socket_type_jewel""), Tuple.Create({itemSocketsMinimum}, {itemSocketsMaximum})),
                                                KeyValuePair.Create((IIdentifier)new StringIdentifier(""socket_type_rune""), Tuple.Create({itemSocketsMinimum}, {itemSocketsMaximum})),
                                            }},
                                            {itemSocketsMaximum}),".Trim()
                                        : string.Empty
                                )}
                                new HasStatsGeneratorComponent(new Dictionary<IIdentifier, double>()
                                {{
                                    [new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("ITEM_LEVEL").StatDefinitionId}"")] = {itemLevel},
                                    [new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("ATTACK_SPEED").StatDefinitionId}"")] = {itemAttackSpeed},
                                    [new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("RANGE").StatDefinitionId}"")] = {itemRange},
                                    // requirements
                                    [new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("REQUIRED_LEVEL").StatDefinitionId}"")] = {itemLevelRequirement},
                                    [new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("REQUIRED_STRENGTH").StatDefinitionId}"")] = {itemStrengthRequirement},
                                    [new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("REQUIRED_DEXTERITY").StatDefinitionId}"")] = {itemDexterityRequirement},
                                    [new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("REQUIRED_INTELLIGENCE").StatDefinitionId}"")] = {itemIntelligenceRequirement},
                                    [new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("REQUIRED_SPEED").StatDefinitionId}"")] = {itemSpeedRequirement},
                                    [new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("REQUIRED_VITALITY").StatDefinitionId}"")] = {itemVitalityRequirement},
                                    // durability
                                    [new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("DURABILITY_MAXIMUM").StatDefinitionId}"")] = {(itemDurabilityMaximum < 1 ? 0 : itemDurabilityMaximum)},
                                    [new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("DURABILITY_CURRENT").StatDefinitionId}"")] = {(itemDurabilityMaximum < 1 ? 0 : itemDurabilityMinimum)},
                                }}),
                                {physicalDamageCode}
                                {magicDamageCode}
                            }})".Trim();
                yield return itemDefinitionCodeTemplate;
            }
        }

        private void ConvertBaseArmor(
            XSSFWorkbook workbook,
            IReadOnlyStatDefinitionToTermMappingRepository statDefinitionToTermMappingRepository)
        {
            var baseArmorSheet = workbook.GetSheet("Base Armor");
            var columnHeaderMapping = _sheetHelper.GetColumnHeaderMapping(baseArmorSheet.GetRow(0));

            var itemDefinitionCodeTemplates = GetArmorItemDefinitionCodeTemplates(
                baseArmorSheet,
                columnHeaderMapping,
                statDefinitionToTermMappingRepository);

            var templatedBaseArmorDefinitionCode = @$"
using System;
using System.Collections.Generic;

using Autofac;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Content.Affixes;
using Macerus.Plugins.Features.GameObjects.Items;
using Macerus.Plugins.Features.GameObjects.Items.Socketing;
using Macerus.Plugins.Features.Inventory.Default.HoverCards;
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
    public sealed class BaseArmorModule : SingleRegistrationModule
    {{
        protected override void SafeLoad(ContainerBuilder builder)
        {{
            builder
                .Register(c =>
                {{
                    var itemDefinitions = new[]
                    {{
{string.Join(",\r\n", itemDefinitionCodeTemplates.Select(x => "						" + x))}
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
            var directoryPath = @"Generated\Items";
            Directory.CreateDirectory(directoryPath);

            var filePath = Path.Combine(directoryPath, "BaseArmorModule.cs");
            File.Delete(filePath);
            File.WriteAllText(filePath, templatedBaseArmorDefinitionCode);
        }

        private IEnumerable<string> GetArmorItemDefinitionCodeTemplates(
            ISheet baseArmorSheet,
            IReadOnlyDictionary<string, int> columnHeaderMapping,
            IReadOnlyStatDefinitionToTermMappingRepository statDefinitionToTermMappingRepository)
        {
            for (int rowIndex = 1; rowIndex < baseArmorSheet.PhysicalNumberOfRows; rowIndex++)
            {
                var row = baseArmorSheet.GetRow(rowIndex);
                var itemId = $"armor_{rowIndex}";

                var itemNameStringResource = row.GetCell(columnHeaderMapping["name"]).StringCellValue;
                var itemNameStringResourceId = $"armor_name_{rowIndex}";

                var itemIconResource = row.GetCell(columnHeaderMapping["icon"]).StringCellValue;
                var itemIconResourceId = $"armor_icon_{rowIndex}";

                var itemEquipSlotName = row.GetCell(columnHeaderMapping["slot"]).StringCellValue;
                var itemEquipSlotId = itemEquipSlotName; // FIXME: convert???

                var itemLevel = _sheetHelper.GetIntValue(row, columnHeaderMapping["item level"]);

                var itemLevelRequirement = _sheetHelper.GetIntValue(row, columnHeaderMapping["level requirement"]);
                var itemStrengthRequirement = _sheetHelper.GetIntValue(row, columnHeaderMapping["req str"]);
                var itemDexterityRequirement = _sheetHelper.GetIntValue(row, columnHeaderMapping["req dex"]);
                var itemIntelligenceRequirement = _sheetHelper.GetIntValue(row, columnHeaderMapping["req int"]);
                var itemSpeedRequirement = _sheetHelper.GetIntValue(row, columnHeaderMapping["req spd"]);
                var itemVitalityRequirement = _sheetHelper.GetIntValue(row, columnHeaderMapping["req vit"]);

                var itemArmorMinimum = _sheetHelper.GetIntValue(row, columnHeaderMapping["armor min"]);
                var itemArmorMaximum = _sheetHelper.GetIntValue(row, columnHeaderMapping["armor max"]);
                var itemEvasionMinimum = _sheetHelper.GetIntValue(row, columnHeaderMapping["evasion min"]);
                var itemEvasionMaximum = _sheetHelper.GetIntValue(row, columnHeaderMapping["evasion max"]);

                var itemDurabilityMinimum = _sheetHelper.GetIntValue(row, columnHeaderMapping["durability min"]);
                var itemDurabilityMaximum = _sheetHelper.GetIntValue(row, columnHeaderMapping["durability max"]);

                var itemSocketsMinimum = _sheetHelper.GetIntValue(row, columnHeaderMapping["sockets min"]);
                var itemSocketsMaximum = _sheetHelper.GetIntValue(row, columnHeaderMapping["sockets max"]);

                var itemDefinitionCodeTemplate = @$"
                        new ItemDefinition(
                            new[]
                            {{
                                AffixFilterAttributes.RequiresNormalAffix,
                                filterContextAmenity.CreateSupportedAttribute(
                                    itemIdentifiers.ItemDefinitionIdentifier,
                                    new StringIdentifier(""{itemId}"")),
                            }},
                            new IGeneratorComponent[]
                            {{
                                new NameGeneratorComponent(""{itemNameStringResourceId}""),
                                new IconGeneratorComponent(""{itemIconResourceId}""),
                                new EquippableGeneratorComponent(new[] {{ new StringIdentifier(""{itemEquipSlotId}"") }}),
                                {(
                                    itemSocketsMaximum > 0
                                        ? @$"new SocketGeneratorComponent(new[]
                                            {{
                                                KeyValuePair.Create((IIdentifier)new StringIdentifier(""socket_type_gem""), Tuple.Create({itemSocketsMinimum}, {itemSocketsMaximum})),
                                                KeyValuePair.Create((IIdentifier)new StringIdentifier(""socket_type_jewel""), Tuple.Create({itemSocketsMinimum}, {itemSocketsMaximum})),
                                                KeyValuePair.Create((IIdentifier)new StringIdentifier(""socket_type_rune""), Tuple.Create({itemSocketsMinimum}, {itemSocketsMaximum})),
                                            }},
                                            {itemSocketsMaximum}),".Trim()
                                        : string.Empty
                                )}
                                new HasStatsGeneratorComponent(new Dictionary<IIdentifier, double>()
                                {{
                                    [new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("ITEM_LEVEL").StatDefinitionId}"")] = {itemLevel},
                                    // requirements
                                    [new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("REQUIRED_LEVEL").StatDefinitionId}"")] = {itemLevelRequirement},
                                    [new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("REQUIRED_STRENGTH").StatDefinitionId}"")] = {itemStrengthRequirement},
                                    [new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("REQUIRED_DEXTERITY").StatDefinitionId}"")] = {itemDexterityRequirement},
                                    [new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("REQUIRED_INTELLIGENCE").StatDefinitionId}"")] = {itemIntelligenceRequirement},
                                    [new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("REQUIRED_SPEED").StatDefinitionId}"")] = {itemSpeedRequirement},
                                    [new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("REQUIRED_VITALITY").StatDefinitionId}"")] = {itemVitalityRequirement},
                                    // durability
                                    [new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("DURABILITY_MAXIMUM").StatDefinitionId}"")] = {(itemDurabilityMaximum < 1 ? 0 : itemDurabilityMaximum)},
                                    [new StringIdentifier(""{statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("DURABILITY_CURRENT").StatDefinitionId}"")] = {(itemDurabilityMaximum < 1 ? 0 : itemDurabilityMinimum)},
                                }}),
                                new RandomStatRangeGeneratorComponent({statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("ARMOR").StatDefinitionId}, {itemArmorMinimum}, {itemArmorMaximum}, 0),
                                new RandomStatRangeGeneratorComponent({statDefinitionToTermMappingRepository.GetStatDefinitionToTermMappingByTerm("EVASION").StatDefinitionId}, {itemEvasionMinimum}, {itemEvasionMaximum}, 0),
                            }})".Trim();
                yield return itemDefinitionCodeTemplate;
            }
        }

        private IReadOnlyStatDefinitionToTermMappingRepository ConvertStats(XSSFWorkbook workbook)
        {
            var statsSheet = workbook.GetSheet("Stats");
            var columnHeaderMapping = _sheetHelper.GetColumnHeaderMapping(statsSheet.GetRow(0));

            var statToTermMapping = new Dictionary<string, string>();
            for (int rowIndex = 1; rowIndex < statsSheet.PhysicalNumberOfRows; rowIndex++)
            {
                var row = statsSheet.GetRow(rowIndex);
                var statDefinitionId = $"stat_{rowIndex}";

                var statTerm = row.GetCell(columnHeaderMapping["term"]).StringCellValue;
                statToTermMapping[statDefinitionId] = statTerm;
            }

            var templatedStatToTermCode = @$"
using System.Collections.Generic;

using Autofac;

using Macerus.Plugins.Features.Combat.Api;
using Macerus.Plugins.Features.GameObjects.Actors;

using ProjectXyz.Api.Framework;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Stats.Default;
using ProjectXyz.Shared.Framework;

namespace Macerus.Content.Generated.Stats
{{
    public sealed class StatsModule : SingleRegistrationModule
    {{
        protected override void SafeLoad(ContainerBuilder builder)
        {{
            builder
                .Register(c =>
                {{
                    var mapping = new Dictionary<IIdentifier, string>()
                    {{
{string.Join(",\r\n", statToTermMapping.Select(x => $"                        [\"{x.Key}\"] = \"{x.Value}\""))}
                    }};
                    var statDefinitionToTermRepository = new InMemoryStatDefinitionToTermMappingRepository(mapping);
                    return statDefinitionToTermRepository;
                }})
                .AsImplementedInterfaces()
                .SingleInstance();
        }}
    }}
}}
";
            var directoryPath = @"Generated\Stats";
            Directory.CreateDirectory(directoryPath);

            var filePath = Path.Combine(directoryPath, "StatsModule.cs");
            File.Delete(filePath);
            File.WriteAllText(filePath, templatedStatToTermCode);

            return new InMemoryStatDefinitionToTermMappingRepository(statToTermMapping
                .ToDictionary(
                    x => (IIdentifier)new StringIdentifier(x.Key),
                    x => x.Value));
        }
    }
}
