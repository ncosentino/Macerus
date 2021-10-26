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

            var statContentConverter = new StatExcelContentConverter(_sheetHelper);
            var statCodeWriter = new StatCodeWriter();

            var affixConverter = new AffixesExcelContentConverter(_sheetHelper);
            var affixCodeWriter = new AffixCodeWriter();

            var rareItemAffixConverter = new RareItemAffixExcelContentConverter(_sheetHelper);
            var rareItemAffixCodeWriter = new RareItemAffixCodeWriter();

            var baseWeaponConverter = new BaseWeaponExcelContentConverter(_sheetHelper);
            var baseWeaponCodeWriter = new BaseWeaponCodeWriter();

            var uniqueItemConverer = new UniqueItemExcelContentConverter(_sheetHelper);
            var uniqueItemCodeWriter = new UniqueItemCodeWriter();

            var enchantmentDefinitionCodeWriter = new EnchantmentDefinitionCodeWriter();

            IEnumerable<StringResourceDto> stringResourceDtos = new List<StringResourceDto>();
            IEnumerable<EnchantmentDefinitionDto> enchantmentDefinitionDtos = new List<EnchantmentDefinitionDto>();
            using (var filestream = File.Open(gameDataSourceLocalFilePath, FileMode.Open, FileAccess.Read))
            {
                var workbook = new XSSFWorkbook(filestream);
                var stats = statContentConverter
                    .ConvertStats(workbook)
                    .ToArray();
                statCodeWriter.WriteStatsCode(stats, outputDirectory);

                var statDefinitionToTermMappingRepository = new InMemoryStatDefinitionToTermMappingRepository(stats.ToDictionary(
                    x => (IIdentifier)new StringIdentifier(x.StatDefinitionId),
                    x => x.StatTerm));

                var affixContent = affixConverter.GetAffixContent(
                    workbook,
                    statDefinitionToTermMappingRepository);
                affixCodeWriter.WriteAffixesCode(
                    affixContent.SelectMany(x => x.AffixDtos),
                    outputDirectory);
                enchantmentDefinitionDtos = enchantmentDefinitionDtos.Concat(affixContent.SelectMany(x => x.EnchantmentDefinitionDtos));
                stringResourceDtos = stringResourceDtos.Concat(affixContent.SelectMany(x => x.StringResourceDtos));

                var rareItemAffixContent = rareItemAffixConverter.GetRareItemAffixesContent(
                    workbook,
                    statDefinitionToTermMappingRepository);
                rareItemAffixCodeWriter.WriteRareItemAffixesCode(
                    rareItemAffixContent.Select(x => x.RareItemAffixDto),
                    outputDirectory);
                stringResourceDtos = stringResourceDtos.Concat(rareItemAffixContent.Select(x => x.StringResourceDto));

                ConvertBaseArmor(workbook, statDefinitionToTermMappingRepository);
                // FIXME: collect image resources

                var baseWeaponConvertedContent = baseWeaponConverter.ConvertBaseWeapons(workbook);
                baseWeaponCodeWriter.WriteBaseWeaponCode(
                    baseWeaponConvertedContent.BaseWeaponDtos,
                    statDefinitionToTermMappingRepository,
                    outputDirectory);
                stringResourceDtos = stringResourceDtos.Concat(baseWeaponConvertedContent.StringResourceDtos);
                // FIXME: collect image resources

                var uniqueItemContent = uniqueItemConverer.GetUniqueItemContent(
                    workbook, 
                    statDefinitionToTermMappingRepository);
                uniqueItemCodeWriter.WriteUniqueItemsCode(
                    uniqueItemContent.Select(x => x.UniqueItemDto),
                    outputDirectory);
                enchantmentDefinitionDtos = enchantmentDefinitionDtos.Concat(uniqueItemContent.SelectMany(x => x.EnchantmentDefinitionDtos));
                stringResourceDtos = stringResourceDtos.Concat(uniqueItemContent.Select(x => x.StringResourceDto));
                // FIXME: collect image resources

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
                                AffixFilterAttributes.AllowsNormalMagicAndRareAffix,
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
    }
}
