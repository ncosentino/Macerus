using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Stats;
using ProjectXyz.Plugins.Features.Stats.Default;
using ProjectXyz.Shared.Framework;

namespace Macerus.ContentConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            var filePath = @"C:\Users\Nick\Downloads\Macerus - Game Data.xlsx";
            using (var filestream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                var workbook = new XSSFWorkbook(filestream);
                var statDefinitionToTermMappingRepository = ConvertStats(workbook);
                ConvertBaseArmor(workbook, statDefinitionToTermMappingRepository);
            }
        }

        private static void ConvertBaseArmor(
            XSSFWorkbook workbook, 
            IReadOnlyStatDefinitionToTermMappingRepository statDefinitionToTermMappingRepository)
        {
            var baseArmorSheet = workbook.GetSheet("Base Armor");
            var columnHeaderMapping = GetColumnHeaderMapping(baseArmorSheet.GetRow(0));
            
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

        private static IEnumerable<string> GetArmorItemDefinitionCodeTemplates(
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

                var itemLevel = GetIntValue(row, columnHeaderMapping["item level"]);

                var itemLevelRequirement = GetIntValue(row, columnHeaderMapping["level requirement"]);
                var itemStrengthRequirement = GetIntValue(row, columnHeaderMapping["req str"]);
                var itemDexterityRequirement = GetIntValue(row, columnHeaderMapping["req dex"]);
                var itemIntelligenceRequirement = GetIntValue(row, columnHeaderMapping["req int"]);
                var itemSpeedRequirement = GetIntValue(row, columnHeaderMapping["req spd"]);
                var itemVitalityRequirement = GetIntValue(row, columnHeaderMapping["req vit"]);

                var itemArmorMinimum = GetIntValue(row, columnHeaderMapping["armor min"]);
                var itemArmorMaximum = GetIntValue(row, columnHeaderMapping["armor max"]);
                var itemEvasionMinimum = GetIntValue(row, columnHeaderMapping["evasion min"]);
                var itemEvasionMaximum = GetIntValue(row, columnHeaderMapping["evasion max"]);

                var itemDurabilityMinimum = GetIntValue(row, columnHeaderMapping["durability min"]);
                var itemDurabilityMaximum = GetIntValue(row, columnHeaderMapping["durability max"]);

                var itemSocketsMinimum = GetIntValue(row, columnHeaderMapping["sockets min"]);
                var itemSocketsMaximum = GetIntValue(row, columnHeaderMapping["sockets max"]);

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

        private static IReadOnlyStatDefinitionToTermMappingRepository ConvertStats(XSSFWorkbook workbook)
        {
            var statsSheet = workbook.GetSheet("Stats");
            var columnHeaderMapping = GetColumnHeaderMapping(statsSheet.GetRow(0));

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

        private static IReadOnlyDictionary<string, int> GetColumnHeaderMapping(IRow row)
        {
            var mapping = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            for (int i =0; i < row.Cells.Count; i++)
            {
                mapping[row.GetCell(i).StringCellValue] = i;
            }

            return mapping;
        }

        private static int GetIntValue(IRow row, int columnIndex)
        {
            var cell = row.GetCell(columnIndex);
            var result = cell?.CellType == CellType.Numeric
                ? (int)cell.NumericCellValue
                : 0;
            return result;
        }
    }
}
