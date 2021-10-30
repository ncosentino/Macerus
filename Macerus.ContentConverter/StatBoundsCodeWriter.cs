using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Macerus.ContentConverter
{
    public sealed class StatBoundsCodeWriter
    {
        public void WriteStatBoundsCode(
           IEnumerable<StatBoundsDto> statBoundsDtos,
           string outputDirectory)
        {
            var templatedStatToTermCode = @$"using System;
using System.Collections.Generic;

using Autofac;

using ProjectXyz.Api.Framework;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Stats.Bounded;
using ProjectXyz.Plugins.Features.Stats.Bounded.Default;

using ProjectXyz.Shared.Framework;


namespace Macerus.Content.Generated.Stats
{{
    public sealed class StatBoundsModule : SingleRegistrationModule
    {{
        protected override void SafeLoad(ContainerBuilder builder)
        {{
            builder
                .Register(c =>
                {{
                    var mapping = new Dictionary<IIdentifier, IStatBounds>()
                    {{
{string.Join(",\r\n", statBoundsDtos.Select(x => 
@$"                        [new StringIdentifier(""{x.StatDefinitionId}"")] = new StatBounds(""{x.MinimumExpression}"", ""{x.MaximumExpression}"")"))}
                    }};
                    var repository = new InMemoryStatDefinitionIdToBoundsMappingRepository(mapping);
                    return repository;
                }})
                .AsImplementedInterfaces()
                .SingleInstance();
        }}
    }}
}}
";
            var directoryPath = Path.Combine(outputDirectory, @"Generated\Stats");
            Directory.CreateDirectory(directoryPath);

            var filePath = Path.Combine(directoryPath, "StatBoundsModule.cs");
            File.Delete(filePath);
            File.WriteAllText(filePath, templatedStatToTermCode);
        }
    }
}
