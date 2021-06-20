using System;

using Macerus.Api.Behaviors;

using Newtonsoft.Json.Linq;

using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Plugins.Data.Newtonsoft.Api;

namespace Macerus.Shared.Behaviors.Serialization.Newtonsoft
{
    public sealed class MovementBehaviorSerializer : IDiscoverableCustomSerializer
    {
        public Type TypeToRegisterFor { get; } = typeof(MovementBehavior);

        public NewtonsoftDeserializeDelegate Deserializer => (
            deserializer,
            stream,
            serializableId) =>
        {
            var jsonObject = (JObject)deserializer.ReadObject(stream);
            var movementBehavior = new MovementBehavior()
            {
                Direction = jsonObject.GetValue("Direction").Value<int>(),
            };
            return movementBehavior;
        };

        public ConvertToSerializableDelegate ConvertToSerializable => (
            serializer,
            objectToConvert,
            visited,
            type,
            serializableId) =>
        {
            var movementBehavior = (IReadOnlyMovementBehavior)objectToConvert;
            var serializable = new Serializable(
                serializableId,
                new
                {
                    // NOTE: we don't actually want to save state like the
                    // current walk path, the velocity, etc...
                    Direction = movementBehavior.Direction,
                });
            return serializable;
        };
    }
}