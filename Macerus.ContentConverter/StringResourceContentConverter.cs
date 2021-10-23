using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Macerus.ContentConverter
{
    public sealed class StringResourceContentConverter : IStringResourceContentConverter
    {
        public void WriteStringResourceModule(
            string namespaceForModule,
            string moduleClassName,
            IEnumerable<KeyValuePair<string, string>> resourceKvps,
            string outputFilePath)
        {
            var stringResourceCode = @$"
using System.Collections.Generic;

using Autofac;

using Macerus.Plugins.Features.Resources.Default;

using ProjectXyz.Api.Framework;
using ProjectXyz.Framework.Autofac;

namespace {namespaceForModule}
{{
    public sealed class {moduleClassName} : SingleRegistrationModule
    {{
        protected override void SafeLoad(ContainerBuilder builder)
        {{
            builder
                .Register(c => new InMemoryStringResourceRepository(new Dictionary<IIdentifier, string>()
                {{

{string.Join(",\r\n", resourceKvps.Select(x => @$"                   [new StringIdentifier(""{x.Key}"")] = ""{x.Value}"""))}
                }}))
                .SingleInstance()
                .AsImplementedInterfaces();
        }}
    }}
}}";

            var directoryPath = Path.GetDirectoryName(outputFilePath);
            Directory.CreateDirectory(directoryPath);

            File.Delete(outputFilePath);
            File.WriteAllText(outputFilePath, stringResourceCode);
        }
    }
}
