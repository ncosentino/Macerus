using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Macerus.ContentConverter
{

    public sealed class RareItemAffixCodeWriter
    {
        public void WriteRareItemAffixesCode(
            IEnumerable<RareItemAffixDto> rareItemAffixDtos,
            string outputDirectory)
        {
            var codeToWrite = @$"using System;
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
{{
    public sealed class RareItemAffixesModule : SingleRegistrationModule
    {{
        protected override void SafeLoad(ContainerBuilder builder)
        {{
            builder
                .Register(c =>
                {{
                    var affixes = new Lazy<IEnumerable<IRareItemAffix>>(() => new IRareItemAffix[]
                    {{
{string.Join(",\r\n", rareItemAffixDtos.Select(x => $"                        {GetRareItrmAffixCodeTemplateFromDto(x)}"))}
                    }});
                    var repository = new InMemoryRareAffixRepository(
                        c.Resolve<Lazy<IAttributeFilterer>>(),
                        affixes);
                    return repository;
                }})
                .SingleInstance()
                .AsImplementedInterfaces();
        }}
    }}
}}";
            var directoryPath = Path.Combine(outputDirectory, @"Generated\Items");
            Directory.CreateDirectory(directoryPath);

            var filePath = Path.Combine(directoryPath, "RareItemAffixesModule.cs");
            File.Delete(filePath);
            File.WriteAllText(filePath, codeToWrite);
        }

        private string GetRareItrmAffixCodeTemplateFromDto(RareItemAffixDto rareItemAffixDto)
        {
            var tagsFilterCode = !rareItemAffixDto.TagFilter.Any()
                ? string.Empty
                : @$"                                new FilterAttribute(
                                    new StringIdentifier(""tags""),
                                    new AnyTagsFilter(new IIdentifier[]
                                    {{
{string.Join(",\r\n", rareItemAffixDto.TagFilter.Select(x => @$"                                        new StringIdentifier(""{x}"")"))}
                                    }}),
                                    true),";
            var codeTemplate = @$"new RareItemAffix(
                            new StringIdentifier(""{rareItemAffixDto.StringResourceId}""), // {rareItemAffixDto.StringResource}
                            new IFilterAttribute[]
                            {{
                                new FilterAttribute(
                                    new StringIdentifier(""is-prefix""),
                                    new BooleanFilterAttributeValue({(rareItemAffixDto.IsPrefix ? "true" : "false")}),
                                    true),
{tagsFilterCode}
                            }})";
            return codeTemplate;
        }
    }
}
