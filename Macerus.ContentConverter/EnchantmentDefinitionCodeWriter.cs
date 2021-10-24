using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Macerus.ContentConverter
{
    public sealed class EnchantmentDefinitionCodeWriter
    {
        public void WriteEnchantmentDefinitionsCode(
            IEnumerable<EnchantmentDefinitionDto> enchantmentDefinitionDtos,
            string outputDirectory)
        {
            var codeToWrite = @$"
using Autofac;

using Macerus.Content.Enchantments;

using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Generation.InMemory;
using ProjectXyz.Shared.Framework;

namespace Macerus.Content.Generated.Enchantments
{{
    public sealed class EnchantmentsModule : SingleRegistrationModule
    {{
        protected override void SafeLoad(ContainerBuilder builder)
        {{
            builder
                .Register(c =>
                {{
                    var enchantmentTemplate = c.Resolve<EnchantmentTemplate>();

                    var enchantmentDefinitions = new[]
                    {{
{string.Join(",\r\n", enchantmentDefinitionDtos.Select(x => $"                        {GetEnchantmentCodeTemplateFromDto(x)}"))}
                    }};
                    var repository = new InMemoryEnchantmentDefinitionRepository(
                        c.Resolve<IAttributeFilterer>(),
                        enchantmentDefinitions);
                    return repository;
                }})
                .SingleInstance()
                .AsImplementedInterfaces();
        }}
    }}
}}";
            var directoryPath = Path.Combine(outputDirectory, @"Generated\Enchantments");
            Directory.CreateDirectory(directoryPath);

            var filePath = Path.Combine(directoryPath, "EnchantmentsModule.cs");
            File.Delete(filePath);
            File.WriteAllText(filePath, codeToWrite);
        }

        private string GetEnchantmentCodeTemplateFromDto(EnchantmentDefinitionDto enchantmentDefinitionDto)
        {
            var enchantmentCodeTemplate = @$"enchantmentTemplate.CreateRangeEnchantment(
                            new StringIdentifier(""{enchantmentDefinitionDto.EnchantmentDefinitionId}""),
                            new StringIdentifier(""{enchantmentDefinitionDto.StatDefinitionId}""), // {enchantmentDefinitionDto.StatTerm}
                            ""{enchantmentDefinitionDto.Modifier}"",
                            {enchantmentDefinitionDto.RangeMinimum},
                            {enchantmentDefinitionDto.RangeMaximum},
                            {enchantmentDefinitionDto.DecimalPlaces})";
            return enchantmentCodeTemplate;
        }
    }
}
