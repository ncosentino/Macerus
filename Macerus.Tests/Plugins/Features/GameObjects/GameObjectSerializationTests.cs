using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Macerus.Api.Behaviors;

using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Shared.Framework;

using Xunit;

namespace Macerus.Tests.Plugins.Features.GameObjects
{
    public sealed class GameObjectSerializationTests
    {
        private static readonly MacerusContainer _container;
        private static readonly TestAmenities _testAmenities;
        private static readonly ISerializer _serializer;
        private static readonly IDeserializer _deserializer;

        static GameObjectSerializationTests()
        {
            _container = new MacerusContainer();
            _testAmenities = new TestAmenities(_container);
            _serializer = _container.Resolve<ISerializer>();
            _deserializer = _container.Resolve<IDeserializer>();
        }

        [Fact]
        private async Task SerializeAndDeserialize_SwampMap_ExpectedResults()
        {
            await _testAmenities.UsingCleanMapAndObjects(async () =>
            {
                var mapManager = _container.Resolve<IMapManager>();
                await mapManager.SwitchMapAsync(new StringIdentifier("swamp"));

                var input = mapManager.ActiveMap;
                var result = Exercise(input);

                Assert.NotNull(result.Item1);
                Assert.False(
                    string.IsNullOrWhiteSpace(result.Item2),
                    "Serialized json was null or whitespace.");
                Assert.Equal(
                    input.Behaviors.Count,
                    result.Item1.Behaviors.Count);
            });
        }

        [Fact]
        private async Task SerializeAndDeserialize_Player_ExpectedResults()
        {
            await _testAmenities.UsingCleanMapAndObjectsWithPlayer(async player =>
            {
                var result = Exercise(player);

                Assert.NotNull(result.Item1);
                Assert.False(
                    string.IsNullOrWhiteSpace(result.Item2),
                    "Serialized json was null or whitespace.");
                Assert.Equal(
                    player.Behaviors.Count,
                    result.Item1.Behaviors.Count);

                // ensure base stats are correct
                var resultBaseStats = result.Item1.Behaviors.GetOnly<IHasStatsBehavior>().BaseStats;
                foreach (var baseStatKvp in player.Behaviors.GetOnly<IHasStatsBehavior>().BaseStats)
                {
                    Assert.Equal(
                        baseStatKvp.Value,
                        resultBaseStats[baseStatKvp.Key]);
                }

                // ensure we have our expected animation
                var animationBehavior = result.Item1.Behaviors.GetOnly<IReadOnlyDynamicAnimationBehavior>();
                Assert.Equal("$actor$", animationBehavior.SourcePattern);
                Assert.Equal(new StringIdentifier("$actor$_stand_forward"), animationBehavior.BaseAnimationId);
                Assert.Equal(0, animationBehavior.CurrentFrameIndex);
                Assert.True(
                    animationBehavior.Visible,
                    $"Expecting {nameof(animationBehavior) + "." + nameof(animationBehavior.Visible)} to be true.");
            });
        }

        private Tuple<IGameObject, string> Exercise(IGameObject gameObjectToSerialize)
        {
            using (var stream = new MemoryStream())
            {
                _serializer.Serialize(
                    stream,
                    gameObjectToSerialize,
                    Encoding.UTF8);
                stream.Seek(0, SeekOrigin.Begin);

                using (var reader = new StreamReader(stream))
                {
                    var rawJson = reader.ReadToEnd();
                    stream.Seek(0, SeekOrigin.Begin);

                    var result = _deserializer.Deserialize<IGameObject>(stream);
                    return Tuple.Create(result, rawJson);
                }
            }
        }
    }
}
