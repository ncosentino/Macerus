using System.IO;
using System.Text;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.GameObjects.Actors;

using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Shared.Framework;

using Xunit;

namespace Macerus.Tests.Plugins.Features.GameObjects.Actors.Serialization.Newtonsoft
{
    public sealed class DynamicAniamtionSerializationTests
    {
        private static readonly ISerializer _serializer;
        private static readonly IDeserializer _deserializer;
        private static readonly IDynamicAnimationBehaviorFactory _dynamicAnimationBehaviorFactory;

        static DynamicAniamtionSerializationTests()
        {
            var container = new MacerusContainer();
            _serializer = container.Resolve<ISerializer>();
            _deserializer = container.Resolve<IDeserializer>();
            _dynamicAnimationBehaviorFactory = container.Resolve<IDynamicAnimationBehaviorFactory>();
        }

        [Fact]
        private void FullSerialize_ValidBehavior_EquivalentDeserialized()
        {
            var behavior = _dynamicAnimationBehaviorFactory.Create(
                new StringIdentifier("base animation id"),
                true,
                123);

            IDynamicAnimationBehavior result;
            using (var stream = new MemoryStream())
            {
                _serializer.Serialize(
                    stream,
                    behavior,
                    Encoding.UTF8);
                stream.Seek(0, SeekOrigin.Begin);
                result = _deserializer.Deserialize<IDynamicAnimationBehavior>(stream);
            }

            Assert.Equal(123, result.CurrentFrameIndex);
            Assert.Equal(new StringIdentifier("base animation id"), result.BaseAnimationId);
            Assert.True(
                result.Visible,
                $"Expecting {nameof(result) + "." + nameof(result.Visible)} to be true.");
        }
    }
}