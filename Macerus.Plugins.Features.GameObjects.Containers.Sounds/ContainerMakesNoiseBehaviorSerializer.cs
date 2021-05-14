using System;

using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Plugins.Data.Newtonsoft.Api;

namespace Macerus.Plugins.Features.GameObjects.Containers.Sounds
{
    public sealed class ContainerMakesNoiseBehaviorSerializer : IDiscoverableCustomSerializer
    {
        private readonly ContainerMakesNoiseBehavior.Factory _factory;

        public ContainerMakesNoiseBehaviorSerializer(ContainerMakesNoiseBehavior.Factory factory)
        {
            _factory = factory;
        }

        public Type TypeToRegisterFor { get; } = typeof(ContainerMakesNoiseBehavior);

        public NewtonsoftDeserializeDelegate Deserializer => (
            deserializer,
            stream,
            serializableId) =>
        {
            var behavior = _factory.Invoke();
            return behavior;
        };

        public ConvertToSerializableDelegate ConvertToSerializable => (
            serializer,
            objectToConvert,
            visited,
            type,
            serializableId) =>
        {
            var behavior = (ContainerMakesNoiseBehavior)objectToConvert;
            var serializable = new Serializable(
                serializableId,
                null);
            return serializable;
        };
    }
}
