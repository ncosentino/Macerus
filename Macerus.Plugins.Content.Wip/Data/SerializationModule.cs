﻿using System;
using System.Collections.Generic;
using System.Linq;

using Autofac;

using Macerus.Plugins.Features.GameObjects.Enchantments;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes; // FIXME: dependency on non-API
using ProjectXyz.Plugins.Features.Enchantments.Generation.MySql;
using ProjectXyz.Shared.Data.Serialization;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.GameObjects.Enchantments.Calculations;

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
                        [typeof(RandomRangeExpressionFilterComponent)] = "RandomRangeExpressionFilterComponent",
                        [typeof(HasStatFilterComponent)] = "HasStatFilterComponent",
                        [typeof(BehaviorFilterComponent)] = "BehaviorFilterComponent",
                        [typeof(FilterAttribute)] = "FilterAttribute",
                        [typeof(StringFilterAttributeValue)] = "StringFilterAttributeValue",
                        [typeof(RangeFilterAttributeValue)] = "RangeFilterAttributeValue",
                        [typeof(CalculationPriority<int>)] = "IntCalculationPriority",
                    };
                    mapping.AddRange(WithCollectionMappings<StringIdentifier>("sid"));
                    mapping.AddRange(WithCollectionMappings<IntIdentifier>("iid"));
                    mapping.AddRange(WithCollectionMappings<IFilterAttribute>("IFilterAttribute"));
                    mapping.AddRange(WithCollectionMappings<IFilterComponent>("IFilterComponent"));
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
