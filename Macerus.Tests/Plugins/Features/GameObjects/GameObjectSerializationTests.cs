using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Macerus.Api.Behaviors;
using Macerus.Api.Behaviors.Filtering;
using Macerus.Api.GameObjects;
using Macerus.Plugins.Features.Encounters;

using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Shared.Framework;

using Xunit;

namespace Macerus.Tests.Plugins.Features.GameObjects.Actors
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
        private void SerializeAndDeserialize_SwampMap_ExpectedResults()
        {
            _testAmenities.UsingCleanMapAndObjects(() =>
            {
                var mapManager = _container.Resolve<IMapManager>();
                mapManager.SwitchMap(new StringIdentifier("swamp"));

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
        private void SerializeAndDeserialize_Player_ExpectedResults()
        {
            _testAmenities.UsingCleanMapAndObjects(() =>
            {
                var mapManager = _container.Resolve<IMapManager>();
                var mapGameObjectManager = _container.Resolve<IReadOnlyMapGameObjectManager>();

                // FIXME: this is just to get the player
                mapManager.SwitchMap(new StringIdentifier("swamp"));
                var player = mapGameObjectManager
                    .GameObjects
                    .Single(x => x.Has<IPlayerControlledBehavior>());

                var result = Exercise(player);

                Assert.NotNull(result.Item1);
                Assert.False(
                    string.IsNullOrWhiteSpace(result.Item2),
                    "Serialized json was null or whitespace.");
                Assert.Equal(
                    player.Behaviors.Count,
                    result.Item1.Behaviors.Count);
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
