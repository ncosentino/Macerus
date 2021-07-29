using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.Audio.SoundGeneration.Default.Engine;
using Macerus.Plugins.Features.Audio.SoundGeneration.Default.Engine.Api;
using Macerus.Plugins.Features.Audio.SoundGeneration.Default.Repository.Api;

namespace Macerus.Plugins.Features.Audio.SoundGeneration.Default.Repository.Transforms
{
    public sealed class ShiftUpTransformation : ISoundPatternTransformation
    {
        public IEnumerable<IWaveInstruction> Transform(IEnumerable<IWaveInstruction> instructions)
        {
            var random = new Random();
            var shiftAmount = random.Next(0x01, 0x05);

            return instructions.Select(x => new WaveInstructionTransform(
                x.Ops[0],
                x.Ops[1],
                x.Ops[2],
                x.Ops[3] + shiftAmount));
        }
    }
}
