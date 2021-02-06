using System;
using System.Collections.Generic;
using System.Linq;

using Autofac;

using Macerus.Plugins.Content.Wip.Enchantments;

using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Enchantments.Generation.MySql;
using ProjectXyz.Shared.Data.Serialization;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.GameObjects.Enchantments.Calculations;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;

namespace Macerus.Plugins.Content.Wip.Data
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
                        [typeof(EnchantmentDefinition)] = "EnchantmentDefinition",
                        [typeof(RandomRangeExpressionGeneratorComponent)] = "RandomRangeExpressionGeneratorComponent",
                        [typeof(HasStatGeneratorComponent)] = "HasStatGeneratorComponent",
                        [typeof(HasPrefixGeneratorComponent)] = "HasPrefixGeneratorComponent",
                        [typeof(HasSuffixGeneratorComponent)] = "HasSuffixGeneratorComponent",
                        [typeof(GeneratorAttribute)] = "GeneratorAttribute",
                        [typeof(StringGeneratorAttributeValue)] = "StringGeneratorAttributeValue",
                        [typeof(RangeGeneratorAttributeValue)] = "RangeGeneratorAttributeValue",
                        [typeof(CalculationPriority<int>)] = "IntCalculationPriority",
                    };
                    mapping.AddRange(WithCollectionMappings<StringIdentifier>("sid"));
                    mapping.AddRange(WithCollectionMappings<IntIdentifier>("iid"));
                    mapping.AddRange(WithCollectionMappings<IGeneratorAttribute>("IGeneratorAttribute"));
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
