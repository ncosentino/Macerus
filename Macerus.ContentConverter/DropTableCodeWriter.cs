using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Macerus.ContentConverter
{
    public sealed class DropTableCodeWriter
    {
        public void WriteDropTableCode(
            IEnumerable<DropTableDto> dropTableDtos,
            string outputDirectory)
        {
            var codeToWrite = @$"using System.Linq;

using Autofac;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Plugins.Features.Encounters;

using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes; // FIXME: dependency on non-API
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables.Implementations.Linked;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables.Linked;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables.Standard;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.InMemory.DropTables;
using ProjectXyz.Shared.Framework;

namespace Macerus.Content.Generated.Items
{{
    public sealed class DropTablesModule : SingleRegistrationModule
    {{
        protected override void SafeLoad(ContainerBuilder builder)
        {{
            builder
                .Register(c =>
                {{
                    var dropTableFactory = c.Resolve<IItemDropTableFactory>();
                    var linkedDropTableFactory = c.Resolve<ILinkedDropTableFactory>();
                    var encounterIdentifiers = c.Resolve<IEncounterIdentifiers>();
                    var filterContextAmenity = c.Resolve<IFilterContextAmenity>();
                    var dropTables = new IDropTable[]
                    {{
{string.Join(",\r\n", dropTableDtos.Select(x => @$"                        {GetDropTableCodeTemplateFromDto(x)}"))}
                    }};
                    return new InMemoryDropTableRepository(dropTables);
                }})
                .AsImplementedInterfaces()
                .SingleInstance();
        }}
    }}
}}";
            var directoryPath = Path.Combine(outputDirectory, @"Generated\Items");
            Directory.CreateDirectory(directoryPath);

            var filePath = Path.Combine(directoryPath, "DropTablesModule.cs");
            File.Delete(filePath);
            File.WriteAllText(filePath, codeToWrite);
        }

        private string GetDropTableCodeTemplateFromDto(DropTableDto dropTableDto)
        {
            var providedAffixFilterCode = !dropTableDto.ProvidedAffixTypes.Any()
                ? string.Empty
                : @$"new FilterAttribute(
                                    new StringIdentifier(""affix-type""),
                                    new AnyStringCollectionFilterAttributeValue({string.Join(", ", dropTableDto.ProvidedAffixTypes.Select(x => @$"""{x}"""))}),
                                    true),";
            var providedItemLevelFilterCode = dropTableDto.ProvidedItemLevel == null
                ? string.Empty
                : @$"new FilterAttribute(
                                    new StringIdentifier(""item-level""),
                                    new DoubleFilterAttributeValue({dropTableDto.ProvidedItemLevel.Value}),
                                    false),";
            var requiredItemLevelFilterCode = @$"new FilterAttribute(
                                    new StringIdentifier(""item-level""),
                                    new DoubleFilterAttributeValue({dropTableDto.RequiredItemLevel}),
                                    true),";

            if (dropTableDto.LinkedTables.Any())
            {
                var linkedDropTableCode = @$"linkedDropTableFactory.Create(
                            new StringIdentifier(""{dropTableDto.DropTableId}""),
                            {dropTableDto.MinimumDrop},
                            {dropTableDto.MinimumDrop},
                            new IWeightedEntry[]
                            {{
{string.Join(",\r\n", dropTableDto.LinkedTables.Select(x => @$"                             new WeightedEntry({x.Value}, new StringIdentifier(""{x.Key}""))"))}
                            }},
                            new IFilterAttribute[]
                            {{
                                {requiredItemLevelFilterCode}
                            }},
                            new IFilterAttribute[]
                            {{
                                {providedAffixFilterCode}
                                {providedItemLevelFilterCode}
                            }})";
                return linkedDropTableCode;
            }

            var itemDropTableCode = @$"dropTableFactory.Create(
                            new StringIdentifier(""{dropTableDto.DropTableId}""),
                            {dropTableDto.MinimumDrop},
                            {dropTableDto.MinimumDrop},
                            new IFilterAttribute[]
                            {{
                                {requiredItemLevelFilterCode}
                            }},
                            new IFilterAttribute[]
                            {{
                                {providedAffixFilterCode}
                                {providedItemLevelFilterCode}
                            }})";
            return itemDropTableCode;
        }
    }
}
