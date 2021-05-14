using System;

using Macerus.Plugins.Features.GameObjects.Actors.Api;

using Newtonsoft.Json.Linq;

using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Data.Newtonsoft.Api;

namespace Macerus.Plugins.Features.GameObjects.Actors.Serialization.Newtonsoft
{
    public sealed class DynamicAnimationBehaviorSerializer : IDiscoverableCustomSerializer
    {
        private readonly IDynamicAnimationBehaviorFactory _dynamicAnimationBehaviorFactory;

        public DynamicAnimationBehaviorSerializer(IDynamicAnimationBehaviorFactory dynamicAnimationBehaviorFactory)
        {
            _dynamicAnimationBehaviorFactory = dynamicAnimationBehaviorFactory;
        }

        public Type TypeToRegisterFor => typeof(DynamicAnimationBehavior);

        public NewtonsoftDeserializeDelegate Deserializer => (
            deserializer,
            stream,
            serializableId) =>
        {
            var jsonObject = (JObject)deserializer.ReadObject(stream);
            var baseAnimationId = deserializer.Deserialize<IIdentifier>((JObject)jsonObject["BaseAnimationId"]);
            var sourcePattern = jsonObject["SourcePattern"].ToString();
            var visible = jsonObject["Visible"].Value<bool>();
            var currentFrameIndex = jsonObject["CurrentFrameIndex"].Value<int>();

            var behavior = _dynamicAnimationBehaviorFactory.Create(
                sourcePattern,
                baseAnimationId,
                visible,
                currentFrameIndex);
            return behavior;
        };

        public ConvertToSerializableDelegate ConvertToSerializable => (
            serializer,
            objectToConvert,
            visited,
            type,
            serializableId) =>
        {
            var behavior = (DynamicAnimationBehavior)objectToConvert;
            var serializable = new Serializable(
                serializableId,
                new
                {
                    BaseAnimationId = serializer.GetObjectToSerialize(
                        behavior.BaseAnimationId,
                        visited),
                    SourcePattern = behavior.SourcePattern,
                    Visible = behavior.Visible,
                    CurrentFrameIndex = behavior.CurrentFrameIndex,
                });
            return serializable;
        };
    }
}
