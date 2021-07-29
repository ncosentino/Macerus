using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.Audio.SoundGeneration.Default.Engine;
using Macerus.Plugins.Features.Audio.SoundGeneration.Default.Engine.Api;
using Macerus.Plugins.Features.Audio.SoundGeneration.Default.Repository.Api;

namespace Macerus.Plugins.Features.Audio.SoundGeneration.Default.Repository.Transforms
{
    public sealed class ExtendTransformation : ISoundPatternTransformation
    {
        public IEnumerable<IWaveInstruction> Transform(IEnumerable<IWaveInstruction> instructions)
        {
            var original = instructions.ToArray();
            if (original.Length < 2)
            {
                return original;
            }

            var random = new Random();
            var index = random.Next(0, original.Length);

            var pivotInstruction = original[index];

            if (index + 1 == original.Length)
            {
                var newInstruction = new WaveInstructionTransform(
                    pivotInstruction.Ops[0],
                    pivotInstruction.Ops[1],
                    pivotInstruction.Ops[2],
                    random.Next() % 2 == 0 ? pivotInstruction.Ops[3] + 0x03 : pivotInstruction.Ops[3] - 0x03);

                return original.AppendSingle(newInstruction);
            }

            var nextInstruction = original[index + 1];

            var delta = (nextInstruction.Ops[3] - pivotInstruction.Ops[3]) / 2;

            var deltaInstruction = new WaveInstructionTransform(
                pivotInstruction.Ops[0],
                pivotInstruction.Ops[1],
                pivotInstruction.Ops[2],
                random.Next() % 2 == 0 ? pivotInstruction.Ops[3] + delta : pivotInstruction.Ops[3] - delta);

            return original.AppendSingle(deltaInstruction);
        }
    }
}
