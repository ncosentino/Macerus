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

            var baseArmorConverter = new BaseArmorExcelContentConverter(_sheetHelper);
            var baseArmorCodeWriter = new BaseArmorCodeWriter();

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

                var baseArmorConvertedContent = baseArmorConverter.ConvertBaseArmors(workbook);
                baseArmorCodeWriter.WriteBaseArmorCode(
                    baseArmorConvertedContent.BaseArmorDtos,
                    statDefinitionToTermMappingRepository,
                    outputDirectory);
                stringResourceDtos = stringResourceDtos.Concat(baseArmorConvertedContent.StringResourceDtos);
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
    }
}
