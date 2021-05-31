using System;
using System.Collections.Generic;
using System.Linq;

using Autofac;

using Macerus.ContentCreator.MapEditor.Behaviors.Shared;
using Macerus.Plugins.Features.Encounters.Triggers;
using Macerus.Plugins.Features.GameObjects.Enchantments;
using Macerus.Plugins.Features.GameObjects.Static.Doors;
using Macerus.Plugins.Features.Mapping;
using Macerus.Shared.Behaviors;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Behaviors.Default;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.Enchantments.Generation;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default.Calculations;
using ProjectXyz.Plugins.Features.GameObjects.Generation.Default;
using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Plugins.Features.Mapping.Default;
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
                        [typeof(EnchantmentDefinition)] = "EnchantmentDefinition",
                        [typeof(RandomRangeExpressionGeneratorComponent)] = "RandomRangeExpressionFilterComponent",
                        [typeof(HasStatGeneratorComponent)] = "HasStatFilterComponent",
                        [typeof(StatefulBehaviorGeneratorComponent)] = "StatefulBehaviorGeneratorComponent",
                        [typeof(StatelessBehaviorGeneratorComponent)] = "StatelessBehaviorGeneratorComponent",
                        [typeof(FilterAttribute)] = "FilterAttribute",
                        [typeof(StringFilterAttributeValue)] = "StringFilterAttributeValue",
                        [typeof(RangeFilterAttributeValue)] = "RangeFilterAttributeValue",
                        [typeof(CalculationPriority<int>)] = "IntCalculationPriority",
                        [typeof(IdentifierBehavior)] = "IdentifierBehavior",
                        [typeof(TypeIdentifierBehavior)] = "TypeIdBehavior",
                        [typeof(TemplateIdentifierBehavior)] = "TemplateIdBehavior",
                        [typeof(HasPrefabResourceIdBehavior)] = "PrefabIdBehavior",
                        [typeof(PositionBehavior)] = "pos",
                        [typeof(SizeBehavior)] = "size",
                        [typeof(BoxColliderBehavior)] = "bcollide",
                        [typeof(CircleColliderBehavior)] = "ccollide",
                        [typeof(DoorInteractableBehavior)] = "DoorBehavior",
                        [typeof(TileResourceBehavior)] = "tileres",
                        [typeof(EncounterTriggerPropertiesBehavior)] = "EncounterTrigger",
                        [typeof(IgnoreSavingGameObjectStateBehavior)] = "IgnoreSavingGameObjectStateBehavior",
                        [typeof(HasFogOfWarBehavior)] = "HasFogOfWarBehavior",
                        [typeof(EditorPrefabResourceIdBehavior)] = "EditorPrefabIdBehavior",
                        [typeof(EditorNameBehavior)] = "ename",
                    };
                    mapping.AddRange(WithCollectionMappings<MapLayersBehavior>("MapLayersBehavior"));
                    mapping.AddRange(WithCollectionMappings<MapLayer>("MapLayer"));
                    mapping.AddRange(WithCollectionMappings<StringIdentifier>("sid"));
                    mapping.AddRange(WithCollectionMappings<IntIdentifier>("iid"));
                    mapping.AddRange(WithCollectionMappings<Interval<double>>("intervald"));
                    mapping.AddRange(WithCollectionMappings<GameObject>("obj"));
                    mapping.AddRange(WithCollectionMappings<IMapLayer>("IMapLayer"));
                    mapping.AddRange(WithCollectionMappings<IIdentifier>("id"));
                    mapping.AddRange(WithCollectionMappings<IGameObject>("iobj"));
                    mapping.AddRange(WithCollectionMappings<IBehavior>("IBehavior"));
                    mapping.AddRange(WithCollectionMappings<IFilterAttribute>("IFilterAttribute"));
                    mapping.AddRange(WithCollectionMappings<IGeneratorComponent>("IGeneratorComponent"));
                    mapping.AddRange(WithCollectionMappings<int>("int"));
                    mapping.AddRange(WithCollectionMappings<double>("dbl"));
                    mapping.AddRange(WithCollectionMappings<float>("flt"));
                    mapping.AddRange(WithCollectionMappings<string>("str"));
                    mapping.AddRange(WithCollectionMappings<bool>("bool"));
                    mapping.AddRange(WithCollectionMappings<byte>("byt"));
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
                typeof(List<T>),
                $"{shortened}[List]");
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
