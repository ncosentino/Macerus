using Autofac;

using ProjectXyz.Api.Framework;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Items.Crafting.Default;
using ProjectXyz.Plugins.Features.GameObjects.Items.Filtering;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Content.Crafting
{
    public sealed class CraftingModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c =>
                {
                    var repository = new InMemoryReplaceIngredientsCraftingRepository(new[]
                    {
                        // 2 junk -> entirely new drop
                        new ReplaceIngredientsCraftingDefinition(
                            new IFilterAttribute[]
                            {
                                // no global state required
                            },
                            new[]
                            {
                                new IFilterAttributeValue[]
                                {
                                    new AnyItemDefinitionIdentifierFilter(new StringIdentifier("junk")),
                                },
                                new IFilterAttributeValue[]
                                {
                                    new AnyItemDefinitionIdentifierFilter(new StringIdentifier("junk")),
                                }
                            },
                            new IIdentifier[]
                            {
                                new StringIdentifier("any_normal_magic_rare_10x_lvl10")
                            })
                    });
                    return repository;
                })
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}