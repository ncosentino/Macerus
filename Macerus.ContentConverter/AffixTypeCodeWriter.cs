using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Macerus.ContentConverter
{
    public sealed class AffixTypeCodeWriter
    {
        public void WriteAffixTypesCode(
            IEnumerable<AffixTypeDto> affixTypeDtos,
            string outputDirectory)
        {
            var codeToWrite = @$"using System;

using Autofac;

using Macerus.Plugins.Features.GameObjects.Items.Affixes.Default;

using ProjectXyz.Framework.Autofac;
using ProjectXyz.Shared.Framework;

namespace Macerus.Content.Generated.Items
{{
    public sealed class AffixTypesModule : SingleRegistrationModule
    {{
        protected override void SafeLoad(ContainerBuilder builder)
        {{
            builder
                .Register(c =>
                {{
                    var affixTypes = new[]
                    {{
{string.Join(",\r\n", affixTypeDtos.Select(x => @$"                        {GetAffixTypeCodeFromDto(x)}"))}
                    }};
                    var repository = new InMemoryAffixTypeRepository(affixTypes);
                    return repository;
                }})
                .SingleInstance()
                .AsImplementedInterfaces();
        }}
    }}
}}";
            var directoryPath = Path.Combine(outputDirectory, @"Generated\Items");
            Directory.CreateDirectory(directoryPath);

            var filePath = Path.Combine(directoryPath, "AffixTypesModule.cs");
            File.Delete(filePath);
            File.WriteAllText(filePath, codeToWrite);
        }

        private string GetAffixTypeCodeFromDto(AffixTypeDto affixTypeDto)
        {
            var code = @$"  new AffixType(new StringIdentifier(""{affixTypeDto.Id}""), ""{affixTypeDto.Name}"")";
            return code;
        }
    }
}
