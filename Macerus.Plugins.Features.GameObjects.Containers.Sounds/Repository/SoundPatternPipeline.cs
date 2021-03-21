using Macerus.Plugins.Features.GameObjects.Containers.Sounds.Engine;
using Macerus.Plugins.Features.GameObjects.Containers.Sounds.Engine.Api;
using Macerus.Plugins.Features.GameObjects.Containers.Sounds.Repository.Api;
using Macerus.Plugins.Features.GameObjects.Containers.Sounds.Repository.Transforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Macerus.Plugins.Features.GameObjects.Containers.Sounds.Repository
{
    public sealed class SoundPatternPipeline
    {
        private readonly Random _random;
        private readonly SoundPatternMaterializer _materializer;
        private readonly SoundPatternRepository _repository;
        private readonly SoundPatternTransformer _transformer;

        public SoundPatternPipeline()
        {
            _random = new Random();
            _materializer = new SoundPatternMaterializer();
            _repository = new SoundPatternRepository();

            // TODO: Right now, these transforms do not abide by the rules published
            // from the engine, but they can (and should).
            // They also modify the 4th Op blindly, which isn't ideal.
            _transformer = new SoundPatternTransformer(
                Tuple.Create<int, ISoundPatternTransformation>(20, new RotateTransformation()),
                Tuple.Create<int, ISoundPatternTransformation>(20, new ReverseTransformation()),
                Tuple.Create<int, ISoundPatternTransformation>(20, new ShortenTransformation()),
                Tuple.Create<int, ISoundPatternTransformation>(20, new ShiftDownTransformation()),
                Tuple.Create<int, ISoundPatternTransformation>(20, new ShiftUpTransformation()));
        }

        public Tuple<IStartingNote, IDutyCycle, ISoundPattern> GetPatternForChannel(Channel channelType)
        {
            var startingNote = _repository
                .StartingNotes
                .Where(x => x.Channel == channelType)
                .Random(_random);

            var dutyCycle = _repository
                .DutyCycles
                .Where(x => x.Channel == channelType)
                .Random(_random);

            var pattern = _repository
                .Patterns
                .Where(x => x.Channel == channelType)
                .Random(_random);

            return Tuple.Create(startingNote, dutyCycle, pattern);
        }

        public ISoundPattern TransformPattern(ISoundPattern pattern)
        {
            return _transformer.Transform(pattern);
        }

        public IWaveChannelDefinition MaterializePatternToChannelDefinition(
            IStartingNote startingNote,
            IDutyCycle dutyCycle,
            ISoundPattern pattern,
            IChannelSpecification channelSpec)
        {
            var materializedNotes = new List<IWaveInstruction>()
            {
                startingNote.Note
            };

            foreach (var instructionTransforms in pattern.Transforms)
            {
                var nextNote = _materializer.Materialize(
                    materializedNotes.Last(),
                    instructionTransforms,
                    channelSpec.OpSpecifications);

                materializedNotes.Add(nextNote);
            }

            var instructions = dutyCycle.Cycle.Ops.Length == 0
                ? materializedNotes
                : new IWaveInstruction[] { dutyCycle.Cycle }.Concat(materializedNotes);

            return new WaveChannelDefinition(
                channelSpec.Channel,
                instructions);
        }
    }
}
