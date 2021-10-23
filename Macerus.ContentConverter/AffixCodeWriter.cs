using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Macerus.ContentConverter
{
    public sealed class AffixCodeWriter
    {
        public void WriteAffixesCode(IEnumerable<AffixDto> affixDtos)
        {
            var codeToWrite = @$"
using System;

using Autofac;

using Macerus.Plugins.Features.GameObjects.Items.Affixes.Default;

using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Shared.Framework;

namespace Macerus.Content.Affixes
{{
    public sealed class AffixesModule : SingleRegistrationModule
    {{
        protected override void SafeLoad(ContainerBuilder builder)
        {{
            builder
                .Register(c =>
                {{
                    var affixTemplate = c.Resolve<AffixTemplate>();
                    var affixDefinitions = new[]
                    {{
{string.Join(",\r\n", affixDtos.Select(x => @$"                        {GetAffixCodeTemplateFromDto(x)}"))}
                    }};
                    var repository = new InMemoryAffixDefinitionRepository(
                        c.Resolve<Lazy<IAttributeFilterer>>(),
                        affixDefinitions);
                    return repository;
                }})
                .SingleInstance()
                .AsImplementedInterfaces();
        }}
    }}
}}";
            var directoryPath = @"Generated\Affixes";
            Directory.CreateDirectory(directoryPath);

            var filePath = Path.Combine(directoryPath, "AffixesModule.cs");
            File.Delete(filePath);
            File.WriteAllText(filePath, codeToWrite);
        }

        private string GetAffixCodeTemplateFromDto(AffixDto affixDto)
        {
            if (string.Equals("magic", affixDto.AffixType, StringComparison.OrdinalIgnoreCase))
            {
                var prefixCode = string.IsNullOrWhiteSpace(affixDto.PrefixStringResourceId)
                    ? "null"
                    : @$"new StringIdentifier(""{affixDto.PrefixStringResourceId}"")";
                var suffixCode = string.IsNullOrWhiteSpace(affixDto.SuffixStringResourceId)
                    ? "null"
                    : @$"new StringIdentifier(""{affixDto.SuffixStringResourceId}"")";
                var magicAffixCodeTemplate = @$"affixTemplate.CreateMagicAffix(
                            new StringIdentifier(""{affixDto.AffixId}""),
                            {affixDto.LevelMinimum},
                            {affixDto.LevelMaximum},
                            {prefixCode},
                            {suffixCode},
                            new[]
                            {{
{string.Join(",\r\n", affixDto.EnchantmentDefinitionIds.Select(x => @$"                                new StringIdentifier(""{x}"")"))}
                            }})";
                return magicAffixCodeTemplate;
            }
            
            if (string.Equals("rare", affixDto.AffixType, StringComparison.OrdinalIgnoreCase))
            {
                var rareAffixCodeTemplate = @$"affixTemplate.CreateRareAffix(
                            new StringIdentifier(""{affixDto.AffixId}""),
                            {affixDto.LevelMinimum},
                            {affixDto.LevelMaximum},
                            new[]
                            {{
{string.Join(",\r\n", affixDto.EnchantmentDefinitionIds.Select(x => @$"                                new StringIdentifier(""{x}"")"))}
                            }})";
                return rareAffixCodeTemplate;
            }

            throw new NotSupportedException(
                $"Unsupported affix type '{affixDto.AffixType}' for affix '{affixDto.AffixId}'.");
        }
    }
}
