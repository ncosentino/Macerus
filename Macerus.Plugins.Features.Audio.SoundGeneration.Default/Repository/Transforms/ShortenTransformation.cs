using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.Audio.SoundGeneration.Default.Engine.Api;
using Macerus.Plugins.Features.Audio.SoundGeneration.Default.Repository.Api;

namespace Macerus.Plugins.Features.Audio.SoundGeneration.Default.Repository.Transforms
{
    public sealed class ShortenTransformation : ISoundPatternTransformation
    {
        public IEnumerable<IWaveInstruction> Transform(IEnumerable<IWaveInstruction> instructions)
        {
            var original = instructions.ToList();
            if (original.Count < 3)
            {
                return original;
            }

            var random = new Random();
            var index = random.Next(1, original.Count);


            original.RemoveAt(index);

            return original;
        }
    }
}
