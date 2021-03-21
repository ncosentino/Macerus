using Macerus.Plugins.Features.GameObjects.Containers.Sounds.Repository.Api;
using NexusLabs.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Macerus.Plugins.Features.GameObjects.Containers.Sounds.Repository
{
    public sealed class SoundPatternTransformer
    {
        private const int MAX_PROBABILITY = 100;
        private const int COMPOSITIONS_TO_APPLY = 3;

        private readonly Dictionary<Predicate<int>, ISoundPatternTransformation> _compositionProbabilityRanges;

        public SoundPatternTransformer(params Tuple<int, ISoundPatternTransformation>[] compositions)
        {
            Contract.Requires(compositions.Sum(x => x.Item1) == MAX_PROBABILITY, $"The composition probabilities must equal {MAX_PROBABILITY}%");

            _compositionProbabilityRanges = new Dictionary<Predicate<int>, ISoundPatternTransformation>();

            var floor = 1;
            foreach (var composition in compositions)
            {
                var currentFloor = floor;
                Predicate<int> predicate = (roll) =>
                    roll >= currentFloor && roll < currentFloor + composition.Item1;

                _compositionProbabilityRanges.Add(predicate, composition.Item2);

                floor += composition.Item1;
            }
        }

        public ISoundPattern Transform(ISoundPattern pattern)
        {
            var random = new Random();
            var instructions = pattern.Transforms.ToArray();

            for (var i = 0; i < COMPOSITIONS_TO_APPLY; i++)
            {
                var roll = random.Next(1, MAX_PROBABILITY);
                var compositionToApply = _compositionProbabilityRanges.FirstOrDefault(x => x.Key.Invoke(roll));

                instructions = compositionToApply.Value.Transform(instructions).ToArray();
            }

            return new SoundPattern(
                pattern.Channel,
                instructions);
        }
    }
}
