using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Macerus.ContentConverter
{
    public sealed class StringResourceCodeWriter : IStringResourceCodeWriter
    {
        public void WriteStringResourceModule(
            IEnumerable<StringResourceDto> stringResourceDtos,
            string outputDirectory)
        {
            var stringResourceCode = @$"
using System.Collections.Generic;

using Autofac;

using Macerus.Plugins.Features.Resources.Default;

using ProjectXyz.Api.Framework;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Shared.Framework;

namespace Macerus.Content.Generated.Resources
{{
    public sealed class StringResourceModule : SingleRegistrationModule
    {{
        protected override void SafeLoad(ContainerBuilder builder)
        {{
            builder
                .Register(c => new InMemoryStringResourceRepository(new Dictionary<IIdentifier, string>()
                {{

{string.Join(",\r\n", stringResourceDtos.Select(x => @$"                   [new StringIdentifier(""{x.StringResourceId}"")] = ""{x.Value}"""))}
                }}))
                .SingleInstance()
                .AsImplementedInterfaces();
        }}
    }}
}}";

            var directoryPath = Path.Combine(outputDirectory, @"Generated\Resources");
            Directory.CreateDirectory(directoryPath);

            var filePath = Path.Combine(directoryPath, "StringResourceModule.cs");
            File.Delete(filePath);
            File.WriteAllText(filePath, stringResourceCode);
        }
    }
}
