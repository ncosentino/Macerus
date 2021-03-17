using System;
using System.Collections.Generic;
using System.Linq;

using Autofac;

using Macerus.Plugins.Features.GameObjects.Enchantments;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default.Calculations;
using ProjectXyz.Plugins.Features.GameObjects.Generation.Default;
using ProjectXyz.Shared.Data.Serialization;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Content.Data
{
    public sealed class SerializationModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c =>
                {
                    var mapping = new Dictionary<Type, string>()
                    {
                        [typeof(Enchantments.EnchantmentDefinition)] = "EnchantmentDefinition2",
                        [typeof(Features.GameObjects.Enchantments.Generation.MySql.EnchantmentDefinition)] = "EnchantmentDefinition1",
                        [typeof(RandomRangeExpressionGeneratorComponent)] = "RandomRangeExpressionFilterComponent",
                        [typeof(HasStatGeneratorComponent)] = "HasStatFilterComponent",
                        [typeof(StatefulBehaviorGeneratorComponent)] = "StatefulBehaviorGeneratorComponent",
                        [typeof(StatelessBehaviorGeneratorComponent)] = "StatelessBehaviorGeneratorComponent",
                        [typeof(FilterAttribute)] = "FilterAttribute",
                        [typeof(StringFilterAttributeValue)] = "StringFilterAttributeValue",
                        [typeof(RangeFilterAttributeValue)] = "RangeFilterAttributeValue",
                        [typeof(CalculationPriority<int>)] = "IntCalculationPriority",
                    };
                    mapping.AddRange(WithCollectionMappings<StringIdentifier>("sid"));
                    mapping.AddRange(WithCollectionMappings<IntIdentifier>("iid"));
                    mapping.AddRange(WithCollectionMappings<IFilterAttribute>("IFilterAttribute"));
                    mapping.AddRange(WithCollectionMappings<IGeneratorComponent>("IGeneratorComponent"));
                    var serializableIdShorterner = new SerializableIdShorterner(mapping);
                    return serializableIdShorterner;
                })
                .SingleInstance()
                .AsImplementedInterfaces();
        }

        private static IEnumerable<KeyValuePair<Type, string>> WithCollectionMappings<T>(string shortened)
        {
            yield return new KeyValuePair<Type, string>(
                typeof(T),
                shortened);
            yield return new KeyValuePair<Type, string>(
                typeof(T[]),
                $"{shortened}[Array]");
            yield return new KeyValuePair<Type, string>(
                typeof(IEnumerable<T>),
                $"{shortened}[Enumerable]");
            yield return new KeyValuePair<Type, string>(
                typeof(IReadOnlyCollection<T>),
                $"{shortened}[ReadOnlyCollection]");
            yield return new KeyValuePair<Type, string>(
                typeof(IReadOnlyList<T>),
                $"{shortened}[ReadOnlyList]");
        }
    }
}
