using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Macerus.ContentConverter
{
    public sealed class StatCodeWriter
    {
        public void WriteStatsCode(
           IEnumerable<StatDto> statDtos,
           string outputDirectory)
        {
            var templatedStatToTermCode = @$"using System;
using System.Collections.Generic;

using Autofac;

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
{string.Join(",\r\n", statDtos.Select(x => $"                        [new StringIdentifier(\"{x.StatDefinitionId}\")] = \"{x.StatTerm}\""))}
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
            var directoryPath = Path.Combine(outputDirectory, @"Generated\Stats");
            Directory.CreateDirectory(directoryPath);

            var filePath = Path.Combine(directoryPath, "StatsModule.cs");
            File.Delete(filePath);
            File.WriteAllText(filePath, templatedStatToTermCode);
        }
    }
}
