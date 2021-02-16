using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autofac;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables.Implementations.Item;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.InMemory.DropTables;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.DropTables
{
    public sealed class DropTableModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            var dropTables = new IDropTable[]
            {
                new ItemDropTable(
                    new StringIdentifier("any_magic_1-10_lvl10"),
                    1,
                    10,
                    Enumerable.Empty<IGeneratorAttribute>(),
                    new[]
                    {
                        new GeneratorAttribute(
                            new StringIdentifier("affix-type"),
                            new StringGeneratorAttributeValue("magic"),
                            true),
                        new GeneratorAttribute(
                            new StringIdentifier("item-level"),
                            new DoubleGeneratorAttributeValue(0),
                            true),
                    }),
            };

            builder
                .Register(x => new InMemoryDropTableRepository(dropTables))
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
